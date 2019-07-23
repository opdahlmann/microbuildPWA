using MicroBuild.Infrastructure.Models;
using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Common.ViewObjects;
using MicroBuild.Management.Domain.Factories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Domain.Factories
{
    public class DoorDetailModelFactory : IDoorDetailModelFactory
    {
        //private readonly string[] PROPS_TO_IGNORE = { "DoorNo", "Id", "ProjectId", "DoorQty", "AttachedProjectDocumentList", "Comment", "Building", "Revision", "MbeProductionId" };
        private readonly string[] PROPS_TO_IGNORE = { "DoorNo", "Id", "ProjectId", "AttachedProjectDocumentList", "Revision", "MbeProductionId" };
        private readonly PropertyInfo[] DOOR_PROP_INFO = typeof(Door).GetProperties();

        public DoorDetailModel GetDoorDetailsViewWithCompanyPermissions(WorkorderDoor door, List<FieldHeader> fieldHeaders, string userName)
        {
            var detailModel = new DoorDetailModel
            {
                Id = door.Id,
                WorkOrderId = door.WorkorderId,
                ProjectId = door.ProjectId,
                DoorNo = door.Door.DoorNo,
                DoorQty = door.Door.DoorQty,
                RoomType = door.Door.RoomType.Content,
                Floor = door.Door.Floor,
                DoorDetails = new List<DoorDetail>()
            };

            var doorSurfDetails = new List<DoorDetail>();

            foreach (PropertyInfo info in DOOR_PROP_INFO)
            {
                if (!PROPS_TO_IGNORE.Contains(info.Name))
                {
                    DoorDetail doorDetail = GetDoorDetail(door, info, fieldHeaders, userName);
                    if (info.PropertyType.Equals(typeof(Hardware)))
                    {
                        DoorDetail doorSurfDetail = GetDoorSurfDetail(door, info, fieldHeaders);
                        if (doorSurfDetail != null)
                        {
                            doorSurfDetails.Add(doorSurfDetail);
                        }
                    }
                    if (doorDetail != null)
                    {
                        detailModel.DoorDetails.Add(doorDetail);
                    }
                }
            }

            if (doorSurfDetails.Count > 0)
            {
                detailModel.DoorDetails.AddRange(doorSurfDetails);
            }
            detailModel.DoorDetails = detailModel.DoorDetails.OrderBy(x => x.Header).ToList();
            return detailModel;
        }

        private DoorDetail GetDoorSurfDetail(WorkorderDoor door, PropertyInfo info, List<FieldHeader> fieldHeaders)
        {
            try
            {
                var value = info.GetValue(door.Door);
                var detail = new DoorDetail();
                var property = info.Name;

                detail.FieldName = property;

                if (value != null)
                {
                    Hardware hardware = (Hardware)value;
                    if (hardware.Surf != null)
                        detail.Content = hardware.Surf.ToString();
                    else
                        return null;

                    var header = fieldHeaders.FirstOrDefault(fh => fh.FieldName.Equals(detail.FieldName) || fh.FieldName.Equals($"{detail.FieldName}.Surf"));
                    if (header == null)
                        return null;
                    detail.Header = (header != null) ? header.Header : property;
                }

                detail.IsMaintainable = false;

                return string.IsNullOrEmpty(detail.Content) ? null : detail;
            }
            catch (TargetParameterCountException ex)
            {
                return null;
            }
        }

        //TODO: Refactor `GetDoorDetail()` (was taken from `DoorUtil.GetDoorDetail()`)

        private DoorDetail GetDoorDetail(WorkorderDoor door, PropertyInfo info, List<FieldHeader> fieldHeaders, string userName)
        {
            try
            {
                var value = info.GetValue(door.Door);
                var detail = new DoorDetail();
                var property = info.Name;

                detail.FieldName = property;
                if (detail.FieldName == "Comment")
                {
                    var header = fieldHeaders.FirstOrDefault(fh => fh.FieldName.Equals(detail.FieldName) || fh.FieldName.Equals($"{detail.FieldName}.Content"));
                    detail.Header = (header != null) ? header.Header : property;
                }

                if (value != null)
                {
                    detail.Content = value.ToString();
                }
                if (detail.FieldName == "ChangedBy")
                {
                    detail.Header = "Endret av";
                    detail.Content = userName;
                }
                else if (detail.FieldName == "ChangedDate")
                {
                    detail.Header = "Endret dato";
                }

                else
                {
                    var header = fieldHeaders.FirstOrDefault(fh => fh.FieldName.Equals(detail.FieldName) || fh.FieldName.Equals($"{detail.FieldName}.Content"));
                    detail.Header = (header != null) ? header.Header : property;
                }

                if (info.PropertyType.Equals(typeof(Hardware)))
                {
                    Hardware hardware = (Hardware)value;
                    detail.IsMaintained = hardware.IsMaintained;
                    detail.Qty = hardware.Qty;
                    detail.IsMaintainable = hardware.IsMaintainable;
                    detail.ChecklistId = hardware.ChecklistId;
                }
                else if (info.PropertyType.Equals(typeof(DoorField)))
                {
                    DoorField doorField = (DoorField)value;
                }

                return string.IsNullOrEmpty(detail.Content) ? null : detail;
            }
            catch (TargetParameterCountException ex)
            {
                return null;
            }
        }
    }
}
