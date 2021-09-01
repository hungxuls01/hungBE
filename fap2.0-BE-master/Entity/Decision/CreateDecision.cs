using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entity.Decision
{
    public class CreateDecision
    {
        public int id { get; set; }
        public string name { get; set; }
        public string note { get; set; }
        public DateTime create_date { get; set; }
        public int categoryid { get; set; }
        public int userid { get; set; }
        public string classs { get; set; }
        public string course { get; set; }
        public string subject { get; set; }
        public string exam { get; set; }
        public float point { get; set; }
        public DateTime attendanceDate { get; set; }
        public string Slot { get; set; }
        public string teacher { get; set; }
        public DateTime dayOfReflection { get; set; }
        public string files { get; set; }
        public DateTime test_date { get; set; }
        public string address_test { get; set; }
        public int level_test { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public string address { get; set; }
        public string organizationName { get; set; }
        public string semester { get; set; }
        public int count_test { get; set; }
        public string part { get; set; }
        public int year_reserve { get; set; }
        public string term_reserve { get; set; }
        public string dear { get; set; }
        public int money { get; set; }
        public string accountHolder { get; set; }
        public string bank { get; set; }
        public string accountID { get; set; }
        public int provinceid { get; set; }
        public string major { get; set; }
        public string semester_start { get; set; }
        public int Specialized_from { get; set; }
        public int campus_from { get; set; }
        public int Specialized_to { get; set; }
        public int campus_to { get; set; }
        public string methodOfPayment { get; set; }
        public DateTime dateOfOut { get; set; }
        public DateTime dateOfBirth { get; set; }
        public string IDCard { get; set; }
        public string rollNumber { get; set; }
        public int year_start { get; set; }
        public string modes { get; set; }
        public string suggestions { get; set; }
    }
}