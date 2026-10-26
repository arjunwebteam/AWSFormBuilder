using System;
using System.Collections.Generic;

namespace AWSFormBuilder.Entities
{
    public class RegistrationCategoriesDropDown
    {
        public Int64 RId { get; set; }

        public Int64 RegistrationCategoriesCategoryId { get; set; }

        public string CategoryName { get; set; }

        public string Title { get; set; }

        public Int32 OrderNo { get; set; }

        public Boolean IsActive { get; set; }

        public Decimal Price { get; set; }

        public string Field1 { get; set; }

        public string Field2 { get; set; }

        public string InsertedBy { get; set; }

        public DateTime InsertedTime { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime UpdatedTime { get; set; }

        public List<RegistrationCategoriesDropDown> lstRegistrationCategoriesDropDown { get; set; }
    }
}