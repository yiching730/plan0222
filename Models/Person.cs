using System;
using System.Collections.Generic;

#nullable disable

namespace plan02.Models
{
    public partial class Person
    {
        public int Pid { get; set; }
        public string Identity { get; set; }
        public string Unit { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public string Birthdate { get; set; }
        public string Address { get; set; }
        public string Tel { get; set; }
        public string Aborignal { get; set; }
        public string Disability { get; set; }
        public string EmergencyPerson { get; set; }
        public string EmergencyTel { get; set; }
        public string OntheJob { get; set; }
        public string OndutyDate { get; set; }
        public string ResignDate { get; set; }
        public string SalarYear { get; set; }
        public string Salaryscale { get; set; }
        public string SalaryIndex { get; set; }
        public string EducationYear { get; set; }
        public string EducationName { get; set; }
        public string EducationDepartment { get; set; }
        public string EducationLevel { get; set; }
        public string EducationStart { get; set; }
        public string EducationEnd { get; set; }
        public string AssessmentYear { get; set; }
        public string AssessmentIndex { get; set; }
        public string TransferYear { get; set; }
        public string TransferIndex { get; set; }
        public string Other { get; set; }
        public string CreateTime { get; set; }
        public string LastChangeName { get; set; }
        public string LastChangeTime { get; set; }
    }
}
