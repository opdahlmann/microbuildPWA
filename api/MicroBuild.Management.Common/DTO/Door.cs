using MicroBuild.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MicroBuild.Management.Common.DTO
{
    #region Door

    public class Door:IEntity
    {
        #region Door Constructure

        public Door()
        {
            RoomType = new RoomType();
            H1 = new Hardware();
            H10 = new Hardware();
            H11 = new Hardware();
            H12 = new Hardware();
            H22 = new Hardware();
            H13 = new Hardware();
            H14 = new Hardware();
            H15 = new Hardware();
            H16 = new Hardware();
            H17 = new Hardware();
            H19 = new Hardware();
            H2 = new Hardware();
            H23 = new Hardware();
            H24 = new Hardware();
            H25 = new Hardware();
            H27 = new Hardware();
            H28 = new Hardware();
            H29 = new Hardware();
            H3 = new Hardware();
            H30 = new Hardware();
            H31 = new Hardware();
            H32 = new Hardware();
            H33 = new Hardware();
            H35 = new Hardware();
            H5 = new Hardware();
            H6 = new Hardware();
            H7 = new Hardware();
            H8 = new Hardware();
            H9 = new Hardware();

            H40 = new Hardware();
            H41 = new Hardware();
            H42 = new Hardware();
            H43 = new Hardware();
            H44 = new Hardware();
            H45 = new Hardware();
            H46 = new Hardware();
            H47 = new Hardware();
            H48 = new Hardware();
            H49 = new Hardware();

            D2 = new DoorField();
            D5 = new DoorField();
            D6 = new DoorField();
            D7 = new DoorField();
            D8 = new DoorField();
            D9 = new DoorField();

            AttachedProjectDocumentList = new List<AttachedProjectDocument>();
        }

        #endregion

        #region Door Properties

        #region Project Documents

        public ICollection<AttachedProjectDocument> AttachedProjectDocumentList { get; set; }

        #endregion

        public string ProjectId { get; set; }
        public int MbeProductionId { get; set; }
        public int? DoorQty { get; set; }
        public string DoorNo { get; set; }
        public string Building { get; set; }
        public string Comment { get; set; }

        public string Revision { get; set; }
        public string RoomNo { get; set; }
        public RoomType RoomType { get; set; }
        public string Func { get; set; }
        public string LR { get; set; }

        public DoorField D2 { get; set; }
        public string D3 { get; set; }
        public string D4 { get; set; }
        public DoorField D5 { get; set; }
        public DoorField D6 { get; set; }
        public DoorField D7 { get; set; }
        public DoorField D8 { get; set; }
        public DoorField D9 { get; set; }
        public string D10 { get; set; }
        public string D11 { get; set; }
        public int? D11Qty { get; set; }
        public string D20 { get; set; }
        public string D21 { get; set; }
        public string D22 { get; set; }
        public string D23 { get; set; }
        public string D24 { get; set; }
        public string D25 { get; set; }
        public string D26 { get; set; }
        public string D27 { get; set; }
        public string D28 { get; set; }

        public string DC1 { get; set; }
        public string DC2 { get; set; }
        public string DC3 { get; set; }
        public string DC4 { get; set; }

        public string Floor { get; set; }
        public string Fly { get; set; }

        #region Hardware Bundle

        public string HB { get; set; }
        public Hardware H1 { get; set; }
        public Hardware H10 { get; set; }
        public Hardware H11 { get; set; }
        public Hardware H12 { get; set; }
        public Hardware H13 { get; set; }
        public Hardware H14 { get; set; }
        public Hardware H15 { get; set; }
        public Hardware H16 { get; set; }
        public Hardware H17 { get; set; }
        public Hardware H19 { get; set; }
        public Hardware H2 { get; set; }
        public Hardware H22 { get; set; }
        public Hardware H23 { get; set; }
        public Hardware H24 { get; set; }
        public Hardware H25 { get; set; }
        public string H26 { get; set; }
        public Hardware H27 { get; set; }
        public Hardware H28 { get; set; }
        public Hardware H29 { get; set; }
        public Hardware H3 { get; set; }
        public Hardware H30 { get; set; }
        public Hardware H31 { get; set; }
        public Hardware H32 { get; set; }
        public Hardware H33 { get; set; }
        public Hardware H35 { get; set; }
        public Hardware H5 { get; set; }
        public Hardware H6 { get; set; }
        public Hardware H7 { get; set; }
        public Hardware H8 { get; set; }
        public Hardware H9 { get; set; }
        public Hardware H40 { get; set; }
        public Hardware H41 { get; set; }
        public Hardware H42 { get; set; }
        public Hardware H43 { get; set; }
        public Hardware H44 { get; set; }

        public Hardware H45 { get; set; }
        public Hardware H46 { get; set; }
        public Hardware H47 { get; set; }
        public Hardware H48 { get; set; }
        public Hardware H49 { get; set; }

        public string HC { get; set; }
        public string DoorBundle { get; set; }

        public DateTime ChangedDate { get; set; }
        public string ChangedBy { get; set; }

        #endregion

        public string Panel { get; set; }

        public string ExternalId1 { get; set; }
        public string ExternalId2 { get; set; }

        public string Secure { get; set; }
        public string URF1 { get; set; }
        public string URF2 { get; set; }
        public string URF3 { get; set; }
        public string URF4 { get; set; }
        public string URF5 { get; set; }
        public string URF6 { get; set; }
        public string URF7 { get; set; }
        public string URF8 { get; set; }

        public string Id { get; set; }
        public string DoorId { get; set; }
        public string Status { get; set; }

        #endregion

        #region Property retrieval

        public object this[string propertyName]
        {
            get
            {
                // probably faster without reflection:
                // like:  return Properties.Settings.Default.PropertyValues[propertyName] 
                // instead of the following
                Type myType = typeof(Door);
                PropertyInfo myPropInfo = myType.GetProperty(propertyName);
                return myPropInfo.GetValue(this, null);

                //if (propertyName.Contains(".Content"))//complex type nested
                //{
                //	var temp = propertyName.Split(new char[] { '.' }, 2);
                //	PropertyInfo myPropInfo = myType.GetProperty(temp[0]);

                //	dynamic _propValue = myPropInfo.GetValue(this, null);
                //	//dynamic propValue;
                //	//if (propertyName.StartsWith("H"))
                //	//	propValue = _propValue as Hardware;
                //	//else if (propertyName.StartsWith("D"))
                //	//	propValue = _propValue as DoorField;
                //	//else if (propertyName.Equals("RoomType"))
                //	//	propValue = _propValue as RoomType;
                //	//else
                //	//	propValue = _propValue as string;

                //	var subProperty = _propValue.Content;
                //	return subProperty;
                //}
                //else if (propertyName.Contains(".Qty"))//complex type nested
                //{
                //	var temp = propertyName.Split(new char[] { '.' }, 2);
                //	PropertyInfo myPropInfo = myType.GetProperty(temp[0]);
                //	dynamic _propValue = myPropInfo.GetValue(this, null);

                //	var subProperty = _propValue.Qty;
                //	return subProperty;
                //}
                //else if (propertyName.Contains(".Surf"))//complex type nested
                //{
                //	var temp = propertyName.Split(new char[] { '.' }, 2);
                //	PropertyInfo myPropInfo = myType.GetProperty(temp[0]);
                //	dynamic _propValue = myPropInfo.GetValue(this, null);

                //	var subProperty = _propValue.Surf;
                //	return subProperty;
                //}
                //else
                //{
                //	//var prop = this.GetType().GetProperty(propertyName);
                //	//return prop != null ? prop.GetValue(this, null) : null;
                //	PropertyInfo myPropInfo = myType.GetProperty(propertyName);
                //	return myPropInfo.GetValue(this, null);
                //}
            }
            set
            {
                Type myType = typeof(Door);
                PropertyInfo myPropInfo = myType.GetProperty(propertyName);
                myPropInfo.SetValue(this, value, null);
            }

        }

        #endregion
    }

    #endregion

    #region Hardware

    public class Hardware
    {
        public string Id { get; set; }
        public bool IsMaintainable { get; set; }
        public int? Qty { get; set; }
        public string Content { get; set; }
        public string Surf { get; set; }
        public bool IsMaintained { get; set; }
		public string ChecklistId { get; set; }

		public override string ToString()
        {
            return Content;
        }

        public override bool Equals(object h)
        {
            var hh = h as Hardware;
            return AreEqual(Qty, hh.Qty) && AreEqual(Content, hh.Content) && AreEqual(Surf, hh.Surf);
        }

        private bool AreEqual(object x, object y)
        {
            return !(x != y && (x == null || !x.Equals(y)));
        }
    }

    #endregion

    #region RoomType

    public class RoomType
    {
        public string Content { get; set; }

        public override string ToString()
        {
            return Content;
        }

        public override bool Equals(object r)
        {
            var rr = r as RoomType;
            return AreEqual(Content, rr.Content);
        }

        private bool AreEqual(object x, object y)
        {
            return !(x != y && (x == null || !x.Equals(y)));
        }
    }

    #endregion
}
