using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entity.Student
{
    public class CreateStudent
    {
        public int id { get; set; }
        public string Account { get; set; }
        public string RollNumber { get; set; }
        public string Image { get; set; }
        public string OldRollNumber { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string FirstName { get; set; }
        public System.DateTime DateOfBirth { get; set; }
        public bool Gender { get; set; }
        public string IDCard { get; set; }
        public Nullable<System.DateTime> DateOfIssue { get; set; }
        public string PlaceOfIssue { get; set; }
        public string Address { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public string ParentName { get; set; }
        public string ParentJob { get; set; }
        public string PlaceOfWork { get; set; }
        public string ParentPhone { get; set; }
        public string ParentAddress { get; set; }
        public string ParentEmail { get; set; }
        public string Major { get; set; }
        public Nullable<short> CurrentTermNo { get; set; }
        public Nullable<short> TermPaid { get; set; }
        public Nullable<int> QD_ChuyenNganh { get; set; }
        public Nullable<int> QD_SV_ChinhQuy { get; set; }
        public Nullable<System.DateTime> Date_SV_ChinhQuy { get; set; }
        public string HanBayNam { get; set; }
        public string LoaiTaiChinh { get; set; }
        public Nullable<int> QD_ThoiHoc { get; set; }
        public Nullable<int> QD_Dieuchuyen { get; set; }
        public Nullable<int> QD_BaoLuu_Exchange { get; set; }
        public Nullable<int> QD_TN { get; set; }
        public Nullable<int> QD_Rejoin { get; set; }
        public string TT_Den { get; set; }
        public Nullable<int> OldCurrentTermNo { get; set; }
        public Nullable<int> Specializedid { get; set; }
        public Nullable<int> Modeid { get; set; }
        public Nullable<int> CapstoneProjectid { get; set; }
        public Nullable<int> Classid { get; set; }
        public Nullable<int> SpecializedOldid { get; set; }
        public Nullable<int> Campusid { get; set; }
        public int roleid { get; set; }
        public Nullable<int> Departmentid { get; set; }
        public Nullable<int> userid { get; set; }

        public bool isRemove { get; set; }
    }
}