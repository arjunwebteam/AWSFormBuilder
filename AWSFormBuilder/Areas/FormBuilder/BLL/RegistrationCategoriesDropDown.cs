using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace AWSFormBuilder.Areas.FormBuilder.BLL
{
  public  class RegistrationCategoriesDropDown
    {
        AWSFormBuilder.Areas.FormBuilder.DAL.RegistrationCategoriesDropDown _RegistrationCategoriesDropDown = new AWSFormBuilder.Areas.FormBuilder.DAL.RegistrationCategoriesDropDown();

        #region Methods

        public Int64 InsertRegistrationCategoriesDropDown(Entities.RegistrationCategoriesDropDown objRegistrationCategoriesDropDown)
        {
            Int64 _status = 0;
            if (objRegistrationCategoriesDropDown != null)
            {
                _status = _RegistrationCategoriesDropDown.InsertRegistrationCategoriesDropDown(objRegistrationCategoriesDropDown);

            }
            return _status;
        }

        public Int64 DeleteRegistrationCategoriesCategory(Int64 RegistrationCategoriesCategoryId)
        {
            Int64 _status = 0;
            _status = _RegistrationCategoriesDropDown.DeleteRegistrationCategoriesCategory(RegistrationCategoriesCategoryId);
            return _status;
        }

        public Int64 UpdateRegistrationCategoriesStatus(Int64 RegistrationCategoriesCategoryId)
        {
            Int64 _status = 0;
            _status = _RegistrationCategoriesDropDown.UpdateRegistrationCategoriesStatus(RegistrationCategoriesCategoryId);
            return _status;
        }

        public Int64 UpdateRegistrationCategoriesDisplayOrder(int DisplayOrder, Int64 RegistrationCategoriesCategoryId)
        {
            Int64 _status = 0;
            _status = _RegistrationCategoriesDropDown.UpdateRegistrationCategoriesDisplayOrder(DisplayOrder, RegistrationCategoriesCategoryId);
            return _status;
        }

        #endregion

        #region Entities filling

        public List<AWSFormBuilder.Entities.RegistrationCategoriesDropDown> GetRegistrationCategoriesDropDownList(ref int status)
        {
            List<AWSFormBuilder.Entities.RegistrationCategoriesDropDown> lstRegistrationCategoriesDropDown = new List<Entities.RegistrationCategoriesDropDown>();
            DataTable dt = _RegistrationCategoriesDropDown.GetRegistrationCategoriesDropDownList(ref status);

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    AWSFormBuilder.Entities.RegistrationCategoriesDropDown objlstRegistrationCategoriesDropDown = new AWSFormBuilder.Entities.RegistrationCategoriesDropDown();

                    objlstRegistrationCategoriesDropDown.RegistrationCategoriesCategoryId = Convert.ToInt64(dr["RegistrationCategoriesCategoryId"].ToString());
                    objlstRegistrationCategoriesDropDown.CategoryName = dr["CategoryName"].ToString();
                    objlstRegistrationCategoriesDropDown.Title = (dr["Title"] != DBNull.Value ? dr["Title"].ToString() : null);
                    objlstRegistrationCategoriesDropDown.OrderNo = (dr["OrderNo"] != DBNull.Value ? Convert.ToInt32(dr["OrderNo"]) : 0);
                    objlstRegistrationCategoriesDropDown.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    objlstRegistrationCategoriesDropDown.Price = (dr["Price"] != DBNull.Value ? Convert.ToDecimal(dr["Price"]) : 0);

                    lstRegistrationCategoriesDropDown.Add(objlstRegistrationCategoriesDropDown);
                }

            }
            return lstRegistrationCategoriesDropDown;
        }

        public AWSFormBuilder.Entities.RegistrationCategoriesDropDown GetRegistrationCategoriesById(Int64 RegistrationCategoriesCategoryId, ref int status)
        {
            AWSFormBuilder.Entities.RegistrationCategoriesDropDown objRegistrationCategoriesDropDown = new AWSFormBuilder.Entities.RegistrationCategoriesDropDown();
            DataTable dt = new DataTable();
            if (RegistrationCategoriesCategoryId != 0)
            {
                dt = _RegistrationCategoriesDropDown.GetRegistrationCategoriesById(RegistrationCategoriesCategoryId, ref status);
                if (dt.Rows.Count == 1)
                {
                    objRegistrationCategoriesDropDown.RegistrationCategoriesCategoryId = Convert.ToInt64(dt.Rows[0]["RegistrationCategoriesCategoryId"].ToString());
                    objRegistrationCategoriesDropDown.CategoryName = dt.Rows[0]["CategoryName"].ToString();
                    objRegistrationCategoriesDropDown.Title = (dt.Rows[0]["Title"] != DBNull.Value ? dt.Rows[0]["Title"].ToString() : null);
                    objRegistrationCategoriesDropDown.OrderNo = (dt.Rows[0]["OrderNo"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["OrderNo"]) : 0);
                    objRegistrationCategoriesDropDown.IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"]);
                    objRegistrationCategoriesDropDown.Price = (dt.Rows[0]["Price"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["Price"]) : 0);
                    objRegistrationCategoriesDropDown.Field1 = (dt.Rows[0]["Field1"] != DBNull.Value ? dt.Rows[0]["Field1"].ToString() : null);
                    objRegistrationCategoriesDropDown.Field2 = (dt.Rows[0]["Field2"] != DBNull.Value ? dt.Rows[0]["Field2"].ToString() : null);
                    objRegistrationCategoriesDropDown.InsertedBy = (dt.Rows[0]["InsertedBy"] != DBNull.Value ? dt.Rows[0]["InsertedBy"].ToString() : null);
                    objRegistrationCategoriesDropDown.InsertedTime = Convert.ToDateTime(dt.Rows[0]["InsertedTime"].ToString());

                }
            }
            return objRegistrationCategoriesDropDown;
        }

        public List<AWSFormBuilder.Entities.RegistrationCategoriesDropDown> GetRegistrationCategoriesDropDownListByVariable(string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            List<AWSFormBuilder.Entities.RegistrationCategoriesDropDown> lstRegistrationCategoriesDropDown = new List<AWSFormBuilder.Entities.RegistrationCategoriesDropDown>();
            DataTable dt = _RegistrationCategoriesDropDown.GetRegistrationCategoriesDropDownListByVariable(Search, Sort, PageNo, Items, ref Total);
            if (dt.Rows.Count == 0 && PageNo != 0)
            {
                dt = _RegistrationCategoriesDropDown.GetRegistrationCategoriesDropDownListByVariable(Search, Sort, PageNo - 1, Items, ref Total);
            }
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    AWSFormBuilder.Entities.RegistrationCategoriesDropDown objRegistrationCategoriesDropDown = new AWSFormBuilder.Entities.RegistrationCategoriesDropDown();

                    objRegistrationCategoriesDropDown.RId = Convert.ToInt64(dr["RId"].ToString());
                    objRegistrationCategoriesDropDown.RegistrationCategoriesCategoryId = Convert.ToInt64(dr["RegistrationCategoriesCategoryId"].ToString());
                    objRegistrationCategoriesDropDown.CategoryName = dr["CategoryName"].ToString();
                    objRegistrationCategoriesDropDown.Title = (dr["Title"] != DBNull.Value ? dr["Title"].ToString() : null);
                    objRegistrationCategoriesDropDown.OrderNo = (dr["OrderNo"] != DBNull.Value ? Convert.ToInt32(dr["OrderNo"]) : 0);
                    objRegistrationCategoriesDropDown.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    objRegistrationCategoriesDropDown.Price = (dr["Price"] != DBNull.Value ? Convert.ToDecimal(dr["Price"]) : 0);
                    objRegistrationCategoriesDropDown.Field1 = (dr["Field1"] != DBNull.Value ? dr["Field1"].ToString() : null);
                    objRegistrationCategoriesDropDown.Field2 = (dr["Field2"] != DBNull.Value ? dr["Field2"].ToString() : null);
                    objRegistrationCategoriesDropDown.InsertedBy = (dr["InsertedBy"] != DBNull.Value ? dr["InsertedBy"].ToString() : null);
                    objRegistrationCategoriesDropDown.InsertedTime = Convert.ToDateTime(dr["InsertedTime"].ToString());

                    lstRegistrationCategoriesDropDown.Add(objRegistrationCategoriesDropDown);
                }
            }
            return lstRegistrationCategoriesDropDown;
        }

        #endregion
    }
}
