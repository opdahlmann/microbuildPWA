using MicroBuild.Management.Common.DTO;
using MicroBuild.Management.Common.ViewObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Domain.Util
{
    public class DoorUtil
    {
        private static string[] propertiesToIgnore = { "DoorNo", "Id", "ProjectId", "DoorQty",
            "AttachedProjectDocumentList", "Comment", "Building", "Revision", "MbeProductionId" };

        public List<DoorDetailModel> GetDoorDetailModels(List<Door> doors, List<FieldHeader> fieldHeaders, List<string> companiesList)
        {
            PropertyInfo[] propertyInfo = typeof(Door).GetProperties();
            List<DoorDetailModel> doorDetails = new List<DoorDetailModel>();

            foreach (Door door in doors)
            {
                DoorDetailModel detailModel = GetDetailModelForDoor(door, propertyInfo, fieldHeaders, companiesList);
                doorDetails.Add(detailModel);
            }

            return doorDetails;
        }

        public DoorDetailModel GetDetailModelForDoor(Door door, PropertyInfo[] propertyInfo, List<FieldHeader> fieldHeaders, List<string> companiesList)
        {
            DoorDetailModel detailModel = new DoorDetailModel();
            detailModel.ProjectId = door.ProjectId;
            detailModel.DoorNo = door.DoorNo;
            detailModel.DoorQty = door.DoorQty;
            detailModel.RoomType = door.RoomType.Content;
            detailModel.Floor = door.Floor;
            detailModel.Id = door.Id;
            detailModel.DoorDetails = new List<DoorDetail>();

            foreach (PropertyInfo info in propertyInfo)
            {
                if (!propertiesToIgnore.Contains(info.Name))
                {
                    DoorDetail doorDetail = GetDoorDetail(door, info, fieldHeaders, companiesList);

                    if (doorDetail != null)
                    {
                        detailModel.DoorDetails.Add(doorDetail);
                    }
                }
            }

            return detailModel;
        }

        private DoorDetail GetDoorDetail(Door door, PropertyInfo info, List<FieldHeader> fieldHeaders, List<string> companiesList)
        {
            try
            {
                object value = info.GetValue(door);
                DoorDetail detail = new DoorDetail();
                string property = info.Name;

                detail.FieldName = property;

                var header = fieldHeaders.FirstOrDefault(fh => fh.FieldName.Equals(detail.FieldName) || fh.FieldName.Equals($"{detail.FieldName}.Content"));
                detail.Header = (header != null) ? header.Header : property;

                if (value != null)
                {
                    detail.Content = value.ToString();
                }

                //detail.CanMount = true; // TODO: based on the rights, this will change
                detail.IsMaintainable = false;

                if (info.PropertyType.Equals(typeof(Hardware)))
                {
                    Hardware hardware = (Hardware)value;
                    detail.IsMaintained = hardware.IsMaintained;
                    ////if (hardware.CompanyId != null)
                    ////    detail.CanMount = companiesList.Contains(hardware.CompanyId) ? true : false;

                }
                else if (info.PropertyType.Equals(typeof(DoorField)))
                {
                    DoorField doorField = (DoorField)value;
                    detail.IsMaintainable = doorField.Mounted;
                }

                return string.IsNullOrEmpty(detail.Content) ? null : detail;
            }
            catch (TargetParameterCountException ex)
            {
                return null;
            }
        }

        public Door SetDoorDetail<T>(Door door, string propertyName, T value)
        {
            PropertyInfo prop = door.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
            if (null != prop && prop.CanWrite)
            {
                prop.SetValue(door, value, null);
            }

            return door;
        }
    }
}
