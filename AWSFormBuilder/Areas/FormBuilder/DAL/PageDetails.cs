using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWSFormBuilder.Areas.FormBuilder.DAL
{
    public class PageDetails
    {
        DBAccess _dbAccess = new DBAccess();
        SqlParameter[] _sqlP;

        #region Method

        public Int64 DeletePageDetails(Int64 PageDetailId)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@PageDetailId",PageDetailId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("PageDetailsDelete", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public Int64 InsertPageDetails(Entities.PageDetails objPageDetails, ref string DocumentUrl)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
            new SqlParameter("@PageDetailId",objPageDetails.PageDetailId),
            new SqlParameter("@Heading",objPageDetails.Heading),
            new SqlParameter("@Description",(objPageDetails.Description!= null?(object)objPageDetails.Description:DBNull.Value.ToString())),
            new SqlParameter("@IsActive",objPageDetails.IsActive),
            new SqlParameter("@PageUrl",(objPageDetails.PageUrl!= null?(object)objPageDetails.PageUrl:DBNull.Value.ToString())),
            new SqlParameter("@OtherUrl",(objPageDetails.OtherUrl!= null?(object)objPageDetails.OtherUrl:DBNull.Value.ToString())),
            new SqlParameter("@DocumentUrl",DocumentUrl),   // ✅ use ref param, not entity property
            new SqlParameter("@Target",(objPageDetails.Target!= null?(object)objPageDetails.Target:DBNull.Value.ToString())),
            new SqlParameter("@PageTitle",(objPageDetails.PageTitle!= null?(object)objPageDetails.PageTitle:DBNull.Value.ToString())),
            new SqlParameter("@MetaDescription",(objPageDetails.MetaDescription!= null?(object)objPageDetails.MetaDescription:DBNull.Value.ToString())),
            new SqlParameter("@MetaKeywords",(objPageDetails.MetaKeywords!= null?(object)objPageDetails.MetaKeywords:DBNull.Value.ToString())),
            new SqlParameter("@TopLine",(objPageDetails.TopLine!= null?(object)objPageDetails.TopLine:DBNull.Value.ToString())),
            new SqlParameter("@InsertedBy",objPageDetails.UpdatedBy),
            new SqlParameter("@InsertedDate",objPageDetails.InsertedDate),
            new SqlParameter("@UpdatedBy",objPageDetails.UpdatedBy),
            new SqlParameter("@UpdatedDate",objPageDetails.UpdatedDate),
            new SqlParameter("@QStatus",0),
            new SqlParameter("@MenuItemId",objPageDetails.MenuItemId),
            new SqlParameter("@ChapterId",objPageDetails.ChapterId),
            new SqlParameter("@DisplayName",(objPageDetails.DisplayName == null ?DBNull.Value:(object)objPageDetails.DisplayName)),
            new SqlParameter("@PageParentId",(objPageDetails.PageParentId == 0 ?DBNull.Value:(object)objPageDetails.PageParentId)),
            new SqlParameter("@Position",(objPageDetails.Position == 0 ?DBNull.Value:(object)objPageDetails.Position)),
            new SqlParameter("@IsFooterBar",(objPageDetails.IsFooterBar)),
            new SqlParameter("@IsMenuBar",(objPageDetails.IsMenuBar )),
            new SqlParameter("@IsQuickLinks",(objPageDetails.IsQuickLinks)),
            new SqlParameter("@IsTopBar",(objPageDetails.IsTopBar)),
            new SqlParameter("@AddPage",(objPageDetails.AddPage!= null?(object)objPageDetails.AddPage:DBNull.Value.ToString())),
            new SqlParameter("@ExistingMenuItemId",(objPageDetails.ExistingMenuItemId == 0 ?DBNull.Value:(object)objPageDetails.ExistingMenuItemId)),
            };

                // @DocumentUrl is index 6
                _sqlP[6].SqlDbType = SqlDbType.NVarChar;
                _sqlP[6].Size = 512;
                _sqlP[6].Direction = System.Data.ParameterDirection.InputOutput;

                // @QStatus is index 16
                _sqlP[16].Direction = System.Data.ParameterDirection.Output;

                _dbAccess.SP_ExecuteScalar("PageDetailsInsert", ref _sqlP);

                _status = Convert.ToInt64(_sqlP[16].Value);

                var returnedDocUrl = _sqlP[6].Value?.ToString();
                if (!string.IsNullOrEmpty(returnedDocUrl))
                {
                    DocumentUrl = returnedDocUrl; // ✅ final SP-computed filename flows back to controller
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        #endregion

        #region Admin Section

        public DataTable GetPageDetailsById(Int64 PageDetailId, ref int status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@PageDetailId",PageDetailId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("PageDetailsGetById", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetPageDetailsListByVariable(Int64 ChapterId, string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@ChapterId",ChapterId),
                    new SqlParameter("@Search",Search),
                    new SqlParameter("@Sort",Sort),
                    new SqlParameter("@PageNo",PageNo),
                    new SqlParameter("@Items",Items),
                    new SqlParameter("@Total",0)
                };
                _sqlP[5].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("PageDetailsGetListByVariable", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[5].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetPageDetailsListById(Int64 ChapterId, Int64 MenuItemId, ref string IDPath, string Heading, string PageUrl, ref Int64 status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@MenuItemId",MenuItemId),
                    new SqlParameter("@ChapterId",ChapterId),
                    new SqlParameter("@IDPath",""),
                    new SqlParameter("@Heading",Heading),
                    new SqlParameter("@PageUrl",PageUrl),
                    new SqlParameter("@QStatus",0)
                };

                _sqlP[2].SqlDbType = SqlDbType.NVarChar;
                _sqlP[2].Size = 512;
                _sqlP[2].Direction = System.Data.ParameterDirection.InputOutput;

                _sqlP[5].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("PageDetailsGetListById", ref _sqlP);
                status = Convert.ToInt64(_sqlP[5].Value);

                IDPath = _sqlP[2].Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable AppGetPageDetailsListById(Int64 ChapterId, Int64 MenuItemId, ref string IDPath, string Heading, string PageUrl, ref Int64 status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@MenuItemId",MenuItemId),
                    new SqlParameter("@ChapterId",ChapterId),
                    new SqlParameter("@IDPath",""),
                    new SqlParameter("@Heading",Heading),
                    new SqlParameter("@PageUrl",PageUrl),
                    new SqlParameter("@QStatus",0)
                };

                _sqlP[2].SqlDbType = SqlDbType.NVarChar;
                _sqlP[2].Size = 512;
                _sqlP[2].Direction = System.Data.ParameterDirection.InputOutput;

                _sqlP[5].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("APPPageDetailsGetListById", ref _sqlP);
                status = Convert.ToInt64(_sqlP[5].Value);

                IDPath = _sqlP[2].Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetBreadCrumbListById(Int64 ChapterId, Int64 MenuItemId, ref Int64 status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@ChapterId",ChapterId),
                    new SqlParameter("@MenuItemId",MenuItemId), 
                    new SqlParameter("@QStatus",0)
                };
                 
                _sqlP[2].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("BreadCrumbeDetailsGetListById", ref _sqlP);
                status = Convert.ToInt64(_sqlP[2].Value);
                 
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetBreadCrumbListByUrl(string PageUrl, ref Int64 status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@PageUrl",PageUrl), 
                    new SqlParameter("@QStatus",0)
                };

                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("GetBreadCrumbListByUrl", ref _sqlP);
                status = Convert.ToInt64(_sqlP[1].Value);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public Int64 UpdatePageDetailsStatus(Int64 PageDetailId)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@PageDetailId",PageDetailId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("PageDetailsUpdateStatus", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        public DataTable GetMenuPagesListById(Int64 MenuItemId, ref int status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[] 
                {
                    new SqlParameter("@MenuItemId",MenuItemId),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("MenuPagesGetListByMenuItemId", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetPageDetailsList(ref int Total)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[] 
                { 
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[0].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("PageDetailsGetList", ref _sqlP);
                Total = Convert.ToInt32(_sqlP[0].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable PageDetailsGetByHeading(string Heading, ref int status)
        {
            DataTable dt = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@Heading",Heading),
                    new SqlParameter("@QStatus",0)
                };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataTable("PageDetailsGetByHeading", ref _sqlP);
                status = Convert.ToInt32(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        public DataSet MenuPagesDetailsGetById(Int64 PageDetailId, ref int status)
        {
            DataSet dt = null;
            try
            {
                _sqlP = new[]
                {
                    new SqlParameter("@QStatus",0),
                    new SqlParameter("@PageDetailId",PageDetailId),
                };
                _sqlP[0].Direction = System.Data.ParameterDirection.Output;
                dt = _dbAccess.GetDataSet("MenuPagesDetailsGetById", ref _sqlP);
                status = Convert.ToInt32(_sqlP[0].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public Int64 PagedetailsRemoveDocumentUrl(Int64 PageDetailId)
        {
            Int64 _status = 0;
            try
            {
                _sqlP = new[]
                    {
                        new SqlParameter("@PageDetailId",PageDetailId),
                        new SqlParameter("@QStatus",0)
                    };
                _sqlP[1].Direction = System.Data.ParameterDirection.Output;
                _dbAccess.SP_ExecuteScalar("PagedetailsRemoveDocumentUrl", ref _sqlP);
                _status = Convert.ToInt64(_sqlP[1].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _status;
        }

        #endregion
    }
}
