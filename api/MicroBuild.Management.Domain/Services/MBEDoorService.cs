using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Data.API;
using MicroBuild.Management.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Domain.Services
{
    public class MbeDoorService : IMBEDoorService
    {
        private IObjectsLocator ObjectLocator;
        private IDataUnitOfWork DataUnitOfWork;

        public MbeDoorService(IDataUnitOfWork dataUnitOfWork, IObjectsLocator objectLocator)
        {
            this.DataUnitOfWork = dataUnitOfWork;
            this.ObjectLocator = objectLocator;
        }

        private void SetProjectId(Door door, string projectId)
        {
            door.Id = null;
            door.ProjectId = projectId;
        }

        private string GetFieldHeader(string fieldName, List<FieldHeader> fieldHeaders)
        {
            var header = fieldHeaders.FirstOrDefault(h => h.FieldName.Equals(fieldName))?.Header;
            if (header == null)
                header = fieldHeaders.FirstOrDefault(h => h.FieldName.Equals($"{fieldName}.Content"))?.Header;

            if (header == null)
                header = $"({fieldName})";

            return header;
        }

		public async Task<DoorImportSuccessStatus> ImportDoors(string mbeProjectId, string projectId)
		{
			var result = new DoorImportSuccessStatus(mbeProjectId);
			List<Door> engDoors =
				await ObjectLocator.DoorImportAdapter.GetDoorsByEngProjectId(mbeProjectId);

			// Validate duplicate door nrs
			var isDuplicateDoorNrExists = false;
			foreach (var engDoor in engDoors)
			{
				isDuplicateDoorNrExists = engDoors.Any(x => x.Id != engDoor.Id && x.DoorNo == engDoor.DoorNo);

				if (isDuplicateDoorNrExists)
				{
					break;
				}
			}
			if (isDuplicateDoorNrExists)
			{
				result.IsSuccess = false;
				// "MicroBuild Engineering prosjektet har dører med like dørnummer. Vi kan derfor ikke importere/synkronisere prosjektet"
				// "This Engineering project has different doors with similar door numbers. Please make them unique and try syncing again."
				result.Error = "MicroBuild Engineering prosjektet har dører med like dørnummer. Vi kan derfor ikke importere prosjektet.";

				return result;
			}
			else
			{
				result.ProjectId = projectId;
				result.IsSuccess = true;
			}

			engDoors.RemoveAll(x => x.DoorQty == 0);
			engDoors.ForEach(d => SetProjectId(d, projectId));
			await this.DataUnitOfWork.DoorRepository.AddDoorsAsync(engDoors);

			return result;
		}

		public async Task<Sync> GetMbeSyncPreview(string projectId, string userId)
		{
			var result = new Sync(projectId, userId);

			var _projectService = ObjectLocator.ProjectService;
			var progProject = await _projectService.GetProject(projectId);
			var mbeProjectId = progProject?.MBEProjectId;

			if (mbeProjectId == null)
			{
				return null;
			}

			var _mbeProjectService = ObjectLocator.MbeProjectService;
			var fieldHeaders = await _mbeProjectService.GetMbeProjectFieldHeaders(mbeProjectId);

			var _doorImportAdapter = ObjectLocator.DoorImportAdapter;
			var engDoors = await _doorImportAdapter.GetDoorsByEngProjectId(mbeProjectId);

			//// Validate duplicate door nrs
			///
			var isDuplicateDoorNrExists = false;
			foreach (var engDoor in engDoors)
			{
				isDuplicateDoorNrExists = engDoors.Any(x => x.Id != engDoor.Id && x.DoorNo == engDoor.DoorNo);

				if (isDuplicateDoorNrExists)
				{
					break;
				}
			}
			if (isDuplicateDoorNrExists)
			{
				result.IsInvalid = true;
				/* "MicroBuild Engineering prosjektet har dører med like dørnummer. Vi kan derfor ikke importere/synkronisere prosjektet" */
				/* "This Engineering project has different doors with similar door numbers. Please make them unique and try syncing again." */
				result.Error = "MicroBuild Engineering prosjektet har dører med like dørnummer. Vi kan derfor ikke synkronisere prosjektet.";
				return result;
			}

			engDoors.RemoveAll(x => x.DoorQty == 0);

			var _doorRepository = DataUnitOfWork.DoorRepository;
			var progDoors = await _doorRepository.GetDoorsByProjectIdAsync(projectId);

			var normalIntFieldNameList = new List<string>()
			{
				"DoorQty", "D11Qty"
			};
			var normalFieldNameList = new List<string>()
			{
				"DoorId", "Building", "Comment", "RoomNo", "Func", "LR",
				"D3", "D4", "D10", "D11", "D20", "D21", "D22", "D23", "D24", "D25", "D26", "D27", "D28",
				"DC1", "DC2", "DC3", "DC4", "Floor", "Fly", "Panel", "ExternalId1", "ExternalId2", "Secure", "URF1", "URF2", "URF3", "Status",
				"HB", "DoorBundle",
				"H26", "HC",
				"URF4","URF5","URF6","URF7","URF8"
			};
			var roomTypeFieldName = "RoomType";
			var doorObjectFieldNameList = new List<string>()
			{
				"D2", "D5", "D6", "D7", "D8", "D9"
			};
			var hardwareFieldNameList = new List<string>()
			{
				"H1", "H10", "H11", "H12", "H13", "H14", "H15",
				"H16", "H17", "H19", "H2", "H22", "H23", "H24",
				"H25", "H27", "H28", "H29", "H3", "H30", "H31",
				"H32", "H33", "H35", "H5", "H6", "H7", "H8", "H9",
				"H40", "H41", "H42", "H43", "H44",
				"H45","H46","H47","H48","H49"
			};

			foreach (var engDoor in engDoors)
			{
				var progDoor = progDoors.FirstOrDefault(d => !string.IsNullOrWhiteSpace(d.DoorNo) && d.DoorNo.Equals(engDoor.DoorNo));

				DoorSyncChange change;

				if (progDoor == null)
				{
					progDoor = new Door();
					progDoor.DoorNo = engDoor.DoorNo;
					change = new DoorSyncChange(progDoor);
				}
				else
				{
					change = new DoorSyncChange(progDoor.DoorNo, progDoor.Id);
				}

				foreach (var fieldName in normalIntFieldNameList)
				{
					AddNormalIntFieldChange(fieldName, engDoor, progDoor, change, fieldHeaders);
				}

				foreach (var fieldName in normalFieldNameList)
				{
					AddNormalFieldChange(fieldName, engDoor, progDoor, change, fieldHeaders);
				}

				AddRoomTypeChange(roomTypeFieldName, engDoor, progDoor, change, fieldHeaders);

				foreach (var fieldName in doorObjectFieldNameList)
				{
					AddDoorObjectFieldChange(fieldName, engDoor, progDoor, change, fieldHeaders);
				}

				foreach (var fieldName in hardwareFieldNameList)
				{
					AddDoorHardwareChange(fieldName, engDoor, progDoor, change, fieldHeaders);
				}

				if ((change.FieldChanges != null && change.FieldChanges.Count() > 0)) result.DoorChanges.Add(change);
			}

			var deletedProgDoors = progDoors.Where(pd => !engDoors.Any(ed => ed.DoorNo.Equals(pd.DoorNo)));
			string doorQtyFieldName = "DoorQty";
			string doorQtyFieldHeader = GetFieldHeader(doorQtyFieldName, fieldHeaders);

			foreach (var deletedDoor in deletedProgDoors)
			{
				if (deletedDoor.DoorQty != null && deletedDoor.DoorQty > 0)
				{
					var change = new DoorSyncChange(deletedDoor.DoorNo, deletedDoor.Id);
					change.IsDoorDeletion = true;
					change.FieldChanges.Add(new DoorSyncChangeField(doorQtyFieldName, doorQtyFieldHeader, deletedDoor.DoorQty, 0));
					result.DoorChanges.Add(change);
				}
			}

			return result;
		}

		public async Task<long> SyncFromMbe(string projectId, string userId)
		{
			long changeCount = 0;

			var syncChanges = await GetMbeSyncPreview(projectId, userId);

			var _doorRepository = DataUnitOfWork.DoorRepository;
			var doors = await _doorRepository.GetDoorsByProjectIdAsync(projectId);

			foreach (var change in syncChanges?.DoorChanges)
			{
				Door door;

				if (change.IsDoorCreation)
				{
					door = new Door();
					door.DoorNo = change.DoorNo;
					door.ProjectId = projectId;
					door.ChangedBy = userId;
				}
				else
				{
					if (change.IsDoorDeletion)
					{
						door = null;
					}
					else
					{
						door = doors.FirstOrDefault(d =>
							!string.IsNullOrWhiteSpace(d.DoorNo)
							&& d.DoorNo.Equals(change.DoorNo)
							&& !string.IsNullOrWhiteSpace(d.ProjectId)
							&& d.ProjectId.Equals(projectId));

						door.ChangedBy = userId;
					}
				}

				if (door != null)
				{
					foreach (var detailChange in change.FieldChanges)
					{
						if (detailChange.IsValidUpdate)
						{
							if (detailChange.FieldName.Contains("."))
							{
								var propPathArray = detailChange.FieldName.Split('.');
								if (propPathArray[0].StartsWith("H"))
								{
									var prop = door[propPathArray[0]] as Hardware;
									if (propPathArray[1].Equals("Content"))
									{
										prop.Content = detailChange.NewValue as string;
										//prop.CompanyId = null;
									}
									else if (propPathArray[1].Equals("Qty"))
									{
										prop.Qty = detailChange.NewValue as int?;
									}
									else if (propPathArray[1].Equals("Surf"))
									{
										prop.Surf = detailChange.NewValue as string;
									}
									door[propPathArray[0]] = prop;
								}
								else if (propPathArray[0].StartsWith("D"))
								{
									var prop = door[propPathArray[0]] as DoorField;
									if (propPathArray[1].Equals("Content"))
									{
										prop.Content = detailChange.NewValue as string;
									}
									door[propPathArray[0]] = prop;
								}
								else if (propPathArray[0].Equals("RoomType"))
								{
									var prop = door[propPathArray[0]] as RoomType;
									if (propPathArray[1].Equals("Content"))
									{
										prop.Content = detailChange.NewValue as string;
									}
									door[propPathArray[0]] = prop;
								}
							}
							else
							{
								door[detailChange.FieldName] = detailChange.NewValue;
							}
						}
					}

					door.ChangedBy = userId;
					door.ChangedDate = DateTime.UtcNow;
				}

				if (change.IsDoorCreation) // door creation
				{
					await _doorRepository.AddDoorsAsync(new List<Door>() { door });
				}
				else
				{
					if (door == null) // door deletion
					{
						await _doorRepository.DeleteById(change.ProgressDoorId);
					}
					else // door update
					{
						await _doorRepository.UpdateOneAsync(door);
					}
				}

				changeCount++;
			}

			var _syncLogService = ObjectLocator.SyncLogService;
			await _syncLogService.SaveSyncLogItem(syncChanges);

			return changeCount;
		}

		#region Add each field change for a door

		private void AddNormalIntFieldChange(string fieldName, Door engDoor, Door progDoor, DoorSyncChange change, List<FieldHeader> fieldHeaders)
		{
			int? engValue = engDoor[fieldName] as int?;
			int? progValue = progDoor[fieldName] as int?;

			if (progValue != engValue && (progValue == null || !progValue.Equals(engValue)))
			{
				var header = GetFieldHeader(fieldName, fieldHeaders);

				change.FieldChanges.Add(new DoorSyncChangeField(fieldName, header, progValue, engValue));
			}
		}

		private void AddNormalFieldChange(string fieldName, Door engDoor, Door progDoor, DoorSyncChange change, List<FieldHeader> fieldHeaders)
		{
			string engValue = engDoor[fieldName] as string;
			string progValue = progDoor[fieldName] as string;

			if (progValue != engValue && !(string.IsNullOrEmpty(engValue) && string.IsNullOrEmpty(progValue)) && (progValue == null || !progValue.Equals(engValue)))
			{
				var header = GetFieldHeader(fieldName, fieldHeaders);

				change.FieldChanges.Add(new DoorSyncChangeField(fieldName, header, progValue, engValue));
			}
		}

		private void AddRoomTypeChange(string fieldName, Door engDoor, Door progDoor, DoorSyncChange change, List<FieldHeader> fieldHeaders)
		{
			var engValue = engDoor[fieldName] as RoomType;
			var progValue = progDoor[fieldName] as RoomType;

			if (progValue != engValue && !(string.IsNullOrEmpty(engValue.Content) && string.IsNullOrEmpty(progValue.Content)) && (progValue == null || !progValue.Equals(engValue)))
			{
				var fname = $"{fieldName}.Content";
				var header = GetFieldHeader(fname, fieldHeaders);

				change.FieldChanges.Add(new DoorSyncChangeField(fname, header, progValue.Content, engValue.Content, true));
			}
		}

		private void AddDoorObjectFieldChange(string fieldName, Door engDoor, Door progDoor, DoorSyncChange change, List<FieldHeader> fieldHeaders)
		{
			var engValue = engDoor[fieldName] as DoorField;
			var progValue = progDoor[fieldName] as DoorField;

			if (progValue != engValue && !(string.IsNullOrEmpty(engValue.Content) && string.IsNullOrEmpty(progValue.Content)) && (progValue == null || !progValue.Equals(engValue)))
			{
				var fname = $"{fieldName}.Content";
				var header = GetFieldHeader(fname, fieldHeaders);

				change.FieldChanges.Add(new DoorSyncChangeField(fname, header, progValue.Content, engValue.Content, true));
			}
		}

		private void AddDoorHardwareChange(string fieldName, Door engDoor, Door progDoor, DoorSyncChange change, List<FieldHeader> fieldHeaders)
		{
			var engValue = engDoor[fieldName] as Hardware;
			var progValue = progDoor[fieldName] as Hardware;

			if (progValue != engValue && !(string.IsNullOrEmpty(engValue.Content) && string.IsNullOrEmpty(progValue.Content)) && (progValue == null || !progValue.Equals(engValue)))
			{
				if (progValue.Content != engValue.Content && !(string.IsNullOrEmpty(engValue.Content) && string.IsNullOrEmpty(progValue.Content)) && (progValue.Content == null || !progValue.Content.Equals(engValue.Content)))
				{
					var fname = $"{fieldName}.Content";
					var header = GetFieldHeader(fname, fieldHeaders);

					if (progValue.IsMaintained)
					{
						change.FieldChanges.Add(new DoorSyncChangeField(fname, header, progValue.Content, engValue.Content, false));
					}
					else
					{
						change.FieldChanges.Add(new DoorSyncChangeField(fname, header, progValue.Content, engValue.Content, true));
					}
				}

				if (progValue.Qty != engValue.Qty && (progValue.Qty == null || !progValue.Qty.Equals(engValue.Qty)))
				{
					var fname = $"{fieldName}.Qty";
					var header = GetFieldHeader(fname, fieldHeaders);

					if (progValue.IsMaintained)
					{
						change.FieldChanges.Add(new DoorSyncChangeField(fname, header, progValue.Qty, engValue.Qty, false));
					}
					else
					{
						change.FieldChanges.Add(new DoorSyncChangeField(fname, header, progValue.Qty, engValue.Qty, true));
					}
				}

				if (progValue.Surf != engValue.Surf && !(string.IsNullOrEmpty(engValue.Surf) && string.IsNullOrEmpty(progValue.Surf)) && (progValue.Surf == null || !progValue.Surf.Equals(engValue.Surf)))
				{
					var fname = $"{fieldName}.Surf";
					var header = GetFieldHeader(fname, fieldHeaders);

					if (progValue.IsMaintained)
					{
						change.FieldChanges.Add(new DoorSyncChangeField(fname, header, progValue.Surf, engValue.Surf, false));
					}
					else
					{
						change.FieldChanges.Add(new DoorSyncChangeField(fname, header, progValue.Surf, engValue.Surf, true));
					}
				}
			}
		}

		#endregion
	}
}
