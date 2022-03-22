using OrchardCore.Entities;
using System;
namespace Survey.Module.Models
{
    public class SurveyModel : Entity
    {
        public int Id { get; set; }
        public Boolean Good { get; set; }
        public Boolean Fair { get; set; }
        public Boolean Unsatisfy { get; set; }
      
        public string Station { get; set; }
     
        public string Ip { set; get; }
        public string User { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
