using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AWSFormBuilder.Areas.FormBuilder.BLL
{
    public class Members
    {
        AWSFormBuilder.Areas.FormBuilder.DAL.Members _Members = new DAL.Members();
        AWSFormBuilder.Areas.FormBuilder.DAL.Members _Members1 = new DAL.Members();

        #region Methods
        public Int64 DeleteMembers(Int64 MemberId)
        {
            Int64 _status = 0;
            if (MemberId != 0)
            {
                _status = _Members.DeleteMember(MemberId);
            }
            return _status;
        }

        public Int64 DeleteMemberOrder(Int64 MemberOrderId)
        {
            Int64 _status = 0;
            if (MemberOrderId != 0)
            {
                _status = _Members.DeleteMemberOrder(MemberOrderId);
            }
            return _status;
        }

        public Int64 DeleteChildInfo(Int64 ChildInfoId)
        {
            Int64 _status = 0;
            if (ChildInfoId != 0)
            {
                _status = _Members.DeleteChildInfo(ChildInfoId);
            }
            return _status;
        }

        public Int64 InsertMembers(Entities.Members objMembers,ref Int64 MemberId,ref string imageurl, ref string receipturl)
        {
            Int64 _status = 0;
            if (objMembers != null)
            {
                _status = _Members.InsertMember(objMembers,ref MemberId,ref imageurl, ref receipturl);
            }
            return _status;
        }

        public Int64 UpdateMemberStatus(Int64 MemberId)
        {
            Int64 _status = 0;
            if (MemberId != 0)
            {
                _status = _Members.UpdateMemberStatus(MemberId);
            }
            return _status;
        }

        public Int64 FEInsertMembers(Entities.Members objMembers, ref Int64 MemberId, ref string imageurl, ref string receipturl)
        {
            Int64 _status = 0;
            if (objMembers != null)
            {
                _status = _Members.FEInsertMember(objMembers, ref MemberId, ref imageurl, ref receipturl);
            }
            return _status;
        }

     

        public Int64 UnlockMember(Int64 MemberId)
        {
            Int64 _status = 0;
            if (MemberId != 0)
            {
                _status = _Members.UnlockMember(MemberId);
            }
            return _status;
        }
        public Int64 UpdateMembers(Entities.Members objMembers)
        {
            Int64 _status = 0;
            if (objMembers != null)
            {
                _status = _Members.UpdateMember(objMembers);
            }
            return _status;
        }

        public Int64 ProfileEmailUpdate(string Email, Int64 UserId)
        {
            Int64 _status = 0;

            _status = _Members.ProfileEmailUpdate(Email, UserId);

            return _status;
        }

        public Int64 UpdateUserProfileImage(Int64 MemberId, ref string ProfileImage)
        {
            Int64 _status = 0;
            if (MemberId != 0)
            {
                _status = _Members.UpdateUserProfileImage(MemberId, ref ProfileImage);
            }
            return _status;
        }

        public Int64 UpdateMemberProfile(Entities.Members objMembers)
        {
            Int64 _status = 0;
            if (objMembers != null)
            {
                _status = _Members.UpdateMemberProfile(objMembers);
            }
            return _status;
        }

        public Int64 DeleteAllMembers(string MemberId)
        {
            Int64 _status = 0;
            _status = _Members.DeleteAllMembers(MemberId);
            return _status;
        }

        public Int64 InsertChildrenInfo(Entities.ChildrenInfo objChildrenInfo)
        {
            Int64 _status = 0;
            if (objChildrenInfo != null)
            {
                _status = _Members.InsertChildrenInfo(objChildrenInfo);
            }
            return _status;
        }

        public Int64 InsertMemberOrder(Entities.MembershipOrders objMembershipOrders)
        {
            Int64 _status = 0;
            if (objMembershipOrders != null)
            {
                _status = _Members.InsertMemberOrder(objMembershipOrders);
            }
            return _status;
        }
        public Int64 InsertMemberSubscriptions(Entities.MemberSubscriptions objMembershipOrders)
        {
            Int64 _status = 0;
            if (objMembershipOrders != null)
            {
                _status = _Members.UpdateMemberSubscriptions(objMembershipOrders);
            }
            return _status;
        }
        public Int64 MemberTransactionIdUpdate(Entities.MembershipOrders objMembershipOrders)
        {
            Int64 _status = 0;
            if (objMembershipOrders != null)
            {
                _status = _Members.MemberTransactionIdUpdate(objMembershipOrders);
            }
            return _status;
        } 
        public Int64 InsertMemberOrderRenewal(Entities.MembershipOrders objMembershipOrders, ref string imageurl, ref string receipturl)
        {
            Int64 _status = 0;
            if (objMembershipOrders != null)
            {
                _status = _Members.InsertMemberOrderRenewal(objMembershipOrders, ref imageurl, ref receipturl);
            }
            return _status;
        }

        public Int64 ChangePassword(Int64 MemberId, string Password, string FType)
        {
            Int64 _status = 0;
            if (MemberId != 0 && Password != null && Password.Trim() != "")
            {
                _status = _Members.ChangePassword(MemberId, Password, FType);
            }
            return _status;
        }

        public string GetPassword(Int64 _Memberid, ref int _qstatus)
        {
            string _password = "";
            DataTable dt = _Members.GetPassword(_Memberid, ref _qstatus);
            if (dt.Rows.Count == 1)
            {
                _password = dt.Rows[0]["Password"].ToString();
            }
            return _password;
        }

        public Int64 UpdateMemberProfileImage(Int64 MemberId, ref string ProfileImage)
        {
            Int64 _status = 0;
            if (MemberId != 0)
            {
                _status = _Members.UpdateMemberProfileImage(MemberId, ref ProfileImage);
            }
            return _status;
        }

        #endregion

        #region Entity Loading

        public List<Entities.Members> GetMembersListByVariable(string Search,Int64 MembershipTypeId,Int64 PaymentStatusId,string StartDate,string EndDate,string ExpiryDate,string IsVolunteer, Int64 ChapterId, string type, string Sort, int PageNo, int Items, ref int Total)
        {
            DataTable dt = _Members.GetMembersListByVariable(Search, MembershipTypeId, PaymentStatusId, StartDate, EndDate, ExpiryDate, IsVolunteer, ChapterId, type, Sort, PageNo, Items, ref Total);
            List<Entities.Members> lstMembers = new List<Entities.Members>();

            if (dt.Rows.Count == 0 && PageNo > 1)
            {
                dt = _Members.GetMembersListByVariable(Search, MembershipTypeId, PaymentStatusId, StartDate, EndDate, ExpiryDate, IsVolunteer, ChapterId, type, Sort, PageNo, Items, ref Total);
            }

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Entities.Members objMembers = new Entities.Members();

                    objMembers.RId = Convert.ToInt64(dr["Rid"]);
                    objMembers.MemberId = Convert.ToInt64(dr["MemberId"]);
                    objMembers.ChapterId = (dr["ChapterId"] != DBNull.Value ? Convert.ToInt64(dr["ChapterId"]) : 0);
                    objMembers.Title = (dr["Title"] != DBNull.Value ? dr["Title"].ToString() : null);
                    objMembers.Email = dr["Email"].ToString();
                    objMembers.FirstName = dr["FirstName"].ToString();
                    objMembers.LastName = dr["LastName"].ToString();
                    objMembers.ProfileImage = (dr["ProfileImage"] != DBNull.Value ? dr["ProfileImage"].ToString() : null);
                    objMembers.Occupation = (dr["Occupation"] != DBNull.Value ? dr["Occupation"].ToString() : null);
                    objMembers.SpouseFirstName = (dr["SpouseFirstName"] != DBNull.Value ? dr["SpouseFirstName"].ToString() : null);
                    objMembers.SpouseLastName = (dr["SpouseLastName"] != DBNull.Value ? dr["SpouseLastName"].ToString() : null);
                    objMembers.SpouseOccupation = (dr["SpouseOccupation"] != DBNull.Value ? dr["SpouseOccupation"].ToString() : null);
                    objMembers.SpouseEmail = (dr["SpouseEmail"] != DBNull.Value ? dr["SpouseEmail"].ToString() : null);
                    objMembers.SpouseCell = (dr["SpouseCell"] != DBNull.Value ? dr["SpouseCell"].ToString() : null);
                    objMembers.Address = (dr["Address"] != DBNull.Value ? dr["Address"].ToString() : null);
                    objMembers.City = (dr["City"] != DBNull.Value ? dr["City"].ToString() : null);
                    objMembers.State = (dr["State"] != DBNull.Value ? dr["State"].ToString() : null);
                    objMembers.ZipCode = (dr["ZipCode"] != DBNull.Value ? dr["ZipCode"].ToString() : null);
                    objMembers.HomePhone = (dr["HomePhone"] != DBNull.Value ? dr["HomePhone"].ToString() : null);
                    objMembers.MobilePhone = (dr["MobilePhone"] != DBNull.Value ? dr["MobilePhone"].ToString() : null);
                    objMembers.IsApproved = Convert.ToBoolean(dr["IsApproved"]);
                    objMembers.IsLockedOut = (dr["IsLockedOut"] != DBNull.Value ? Convert.ToBoolean(dr["IsLockedOut"]) : false);
                    objMembers.IsActivated = (dr["IsActivated"] != DBNull.Value ?Convert.ToBoolean(dr["IsActivated"]) : false);
                    objMembers.DateActivated = (dr["DateActivated"] != DBNull.Value ?Convert.ToDateTime(dr["DateActivated"]) : DateTime.MinValue);
                    objMembers.MembershipTypeId = (dr["MembershipTypeId"] != DBNull.Value ? Convert.ToInt64(dr["MembershipTypeId"]) : 0);
                    objMembers.MembershipType = dr["MembershipType"].ToString();
                    objMembers.IsVolunteer = (dr["IsVolunteer"] != DBNull.Value ? Convert.ToBoolean(dr["IsVolunteer"]) : false);
                    objMembers.IsTeluguorigin = (dr["IsTeluguorigin"] != DBNull.Value ? Convert.ToBoolean(dr["IsTeluguorigin"]) : false);
                    objMembers.Comments = (dr["Comments"] != DBNull.Value ? dr["Comments"].ToString() : null);
                    objMembers.ReferredBy = (dr["ReferredBy"] != DBNull.Value ? dr["ReferredBy"].ToString() : null);
                    objMembers.MobilePhone = (dr["MobilePhone"] != DBNull.Value ? dr["MobilePhone"].ToString() : null);
                    objMembers.InsertedTime = Convert.ToDateTime(dr["InsertedTime"]);
                    objMembers.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"]);
                    objMembers.objMembershipOrder.Amount = (dr["Amount"] != DBNull.Value ? Convert.ToDecimal(dr["Amount"]) : 0);
                    objMembers.objMembershipOrder.TransactionId = (dr["TransactionId"] != DBNull.Value ? dr["TransactionId"].ToString() : null);
                    objMembers.objMembershipOrder.MembershipTypeId = (dr["MembershipTypeId"] != DBNull.Value ? Convert.ToInt64(dr["MembershipTypeId"]) : 0);
                    objMembers.objMembershipOrder.PaymentStatusId = (dr["PaymentStatusId"] != DBNull.Value ? Convert.ToInt64(dr["PaymentStatusId"]) : 0);
                    objMembers.objMembershipOrder.PaymentMethodId = (dr["MembershipTypeId"] != DBNull.Value ? Convert.ToInt64(dr["MembershipTypeId"]) : 0);
                    objMembers.objMembershipOrder.MembershipTypeId = (dr["PaymentMethodId"] != DBNull.Value ? Convert.ToInt64(dr["PaymentMethodId"]) : 0);
                    objMembers.objMembershipOrder.AdminComment = (dr["AdminComment"] != DBNull.Value ? dr["AdminComment"].ToString() : null);
                    objMembers.objMembershipOrder.UserComment = (dr["UserComment"] != DBNull.Value ? dr["UserComment"].ToString() : null);
                    objMembers.objMembershipOrder.PaymentStatus = (dr["PaymentStatus"] != DBNull.Value ? dr["PaymentStatus"].ToString() : null);
                    objMembers.objMembershipOrder.PaymentMethod = (dr["PaymentMethod"] != DBNull.Value ? dr["PaymentMethod"].ToString() : null);
                    objMembers.objMembershipOrder.OrderDate = (dr["OrderDate"] != DBNull.Value ? Convert.ToDateTime(dr["OrderDate"]) : DateTime.MinValue);
                    objMembers.objMembershipOrder.ExpiryDate = (dr["ExpiryDate"] != DBNull.Value ? Convert.ToDateTime(dr["ExpiryDate"]) : DateTime.MinValue);
                    objMembers.Fax = (dr["Fax"] != DBNull.Value ? dr["Fax"].ToString() : null);
                    objMembers.WebsiteAddress = (dr["WebsiteAddress"] != DBNull.Value ? dr["WebsiteAddress"].ToString() : null);
                    objMembers.Address2 = (dr["Address2"] != DBNull.Value ? dr["Address2"].ToString() : null);
                    objMembers.MemberSkils = (dr["MemberSkils"] != DBNull.Value ? dr["MemberSkils"].ToString() : null);

                    //objMembers.ChapterName = (dr["ChapterName"] != DBNull.Value ? dr["ChapterName"].ToString() : null);


                    lstMembers.Add(objMembers);
                }
            }
            return lstMembers;
        }

        public List<Entities.Members> GetMembersOrderDetailsListByVariable(string Search,string Sort, int PageNo, int Items, ref int Total)
        {
            DataTable dt = _Members.GetMembersOrderDetailsListByVariable(Search,Sort, PageNo, Items, ref Total);
            List<Entities.Members> lstMembers = new List<Entities.Members>();

            if (dt.Rows.Count == 0 && PageNo > 1)
            {
                dt = _Members.GetMembersOrderDetailsListByVariable(Search, Sort, PageNo, Items, ref Total);
            }

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Entities.Members objMembers = new Entities.Members();

                    objMembers.RId = Convert.ToInt64(dr["Rid"]);
                    objMembers.MemberId = Convert.ToInt64(dr["MemberId"]);
                    objMembers.SpouseCell = (dr["SpouseCell"] != DBNull.Value ? dr["SpouseCell"].ToString() : "");
                    objMembers.objMembershipOrder.MembershipOrderId = Convert.ToInt64(dr["MembershipOrderId"]);
                    objMembers.FirstName = dr["FirstName"].ToString();
                    objMembers.MembershipTypeId = Convert.ToInt64(dr["MembershipTypeId"]);
                    objMembers.MembershipType = dr["MembershipType"].ToString();
                    objMembers.objMembershipOrder.Amount = (dr["Amount"] != DBNull.Value ? Convert.ToDecimal(dr["Amount"]) : 0);
                    objMembers.objMembershipOrder.TransactionId = (dr["TransactionId"] != DBNull.Value ? dr["TransactionId"].ToString() : "");
                    objMembers.objMembershipOrder.MembershipTypeId = (dr["MembershipTypeId"] != DBNull.Value ? Convert.ToInt64(dr["MembershipTypeId"]) : 0);
                    objMembers.objMembershipOrder.PaymentStatusId = (dr["PaymentStatusId"] != DBNull.Value ? Convert.ToInt64(dr["PaymentStatusId"]) : 0);
                    objMembers.objMembershipOrder.PaymentMethodId = (dr["MembershipTypeId"] != DBNull.Value ? Convert.ToInt64(dr["MembershipTypeId"]) : 0);
                    objMembers.objMembershipOrder.MembershipTypeId = (dr["PaymentMethodId"] != DBNull.Value ? Convert.ToInt64(dr["PaymentMethodId"]) : 0);
                    objMembers.objMembershipOrder.AdminComment = (dr["AdminComment"] != DBNull.Value ? dr["AdminComment"].ToString() : "");
                    objMembers.objMembershipOrder.PaymentBy = (dr["PaymentBy"] != DBNull.Value ? dr["PaymentBy"].ToString() : "");
                    objMembers.objMembershipOrder.UserComment = (dr["UserComment"] != DBNull.Value ? dr["UserComment"].ToString() : "");
                    objMembers.objMembershipOrder.PaymentStatus = (dr["PaymentStatus"] != DBNull.Value ? dr["PaymentStatus"].ToString() : "");
                    objMembers.objMembershipOrder.PaymentMethod = (dr["PaymentMethod"] != DBNull.Value ? dr["PaymentMethod"].ToString() : "");
                    objMembers.objMembershipOrder.OrderDate = (dr["OrderDate"] != DBNull.Value ? Convert.ToDateTime(dr["OrderDate"]) : DateTime.MinValue);
                    objMembers.objMembershipOrder.ExpiryDate = (dr["ExpiryDate"] != DBNull.Value ? Convert.ToDateTime(dr["ExpiryDate"]) : DateTime.UtcNow);
                    objMembers.objMembershipOrder.UpdatedTime = (dr["UpdatedTime"] != DBNull.Value ? Convert.ToDateTime(dr["UpdatedTime"]) : DateTime.MinValue);
                    objMembers.MemberSkils = (dr["MemberSkils"] != DBNull.Value ? dr["MemberSkils"].ToString() : null);
                    objMembers.Email = (dr["Email"] != DBNull.Value ? dr["Email"].ToString() : null);
                    lstMembers.Add(objMembers);
                }
            }
            return lstMembers;
        }

        public Entities.Members GetMembersById(Int64 MembersId, ref int status)
        {
            DataTable dt = _Members.GetMemberById(MembersId, ref status);
            Entities.Members objMembers = new Entities.Members();

            if (dt.Rows.Count == 1)
            {
                    objMembers.MemberId = Convert.ToInt64(dt.Rows[0]["MemberId"]);
                    objMembers.ChapterId = (dt.Rows[0]["ChapterId"] != DBNull.Value ? Convert.ToInt64(dt.Rows[0]["ChapterId"]) : 0);
                    objMembers.Title = (dt.Rows[0]["Title"] != DBNull.Value ? dt.Rows[0]["Title"].ToString() : null);
                    objMembers.Email = dt.Rows[0]["Email"].ToString();
                    objMembers.FirstName = dt.Rows[0]["FirstName"].ToString();
                    objMembers.LastName = dt.Rows[0]["LastName"].ToString();
                    objMembers.ProfileImage = (dt.Rows[0]["ProfileImage"] != DBNull.Value ? dt.Rows[0]["ProfileImage"].ToString() : null);
                    objMembers.Occupation = (dt.Rows[0]["Occupation"] != DBNull.Value ? dt.Rows[0]["Occupation"].ToString() : null);
                    objMembers.MemberAge = (dt.Rows[0]["MemberAge"] != DBNull.Value ? dt.Rows[0]["MemberAge"].ToString() : null);
                    objMembers.MemberSkils = (dt.Rows[0]["Occupation"] != DBNull.Value ? dt.Rows[0]["MemberSkils"].ToString() : null);
                    objMembers.SpouseSkils = (dt.Rows[0]["SpouseSkils"] != DBNull.Value ? dt.Rows[0]["SpouseSkils"].ToString() : null);
                    objMembers.SpouseFirstName = (dt.Rows[0]["SpouseFirstName"] != DBNull.Value ? dt.Rows[0]["SpouseFirstName"].ToString() : null);
                    objMembers.SpouseLastName = (dt.Rows[0]["SpouseLastName"] != DBNull.Value ? dt.Rows[0]["SpouseLastName"].ToString() : null);
                    objMembers.SpouseOccupation = (dt.Rows[0]["SpouseOccupation"] != DBNull.Value ? dt.Rows[0]["SpouseOccupation"].ToString() : null);
                    objMembers.SpouseEmail = (dt.Rows[0]["SpouseEmail"] != DBNull.Value ? dt.Rows[0]["SpouseEmail"].ToString() : null);
                    objMembers.SpouseCell = (dt.Rows[0]["SpouseCell"] != DBNull.Value ? dt.Rows[0]["SpouseCell"].ToString() : null);
                    objMembers.Address = (dt.Rows[0]["Address"] != DBNull.Value ? dt.Rows[0]["Address"].ToString() : null);
                    objMembers.City = (dt.Rows[0]["City"] != DBNull.Value ? dt.Rows[0]["City"].ToString() : null);
                    objMembers.State = (dt.Rows[0]["State"] != DBNull.Value ? dt.Rows[0]["State"].ToString() : null);
                    objMembers.ZipCode = (dt.Rows[0]["ZipCode"] != DBNull.Value ? dt.Rows[0]["ZipCode"].ToString() : null);
                    objMembers.HomePhone = (dt.Rows[0]["HomePhone"] != DBNull.Value ? dt.Rows[0]["HomePhone"].ToString() : null);
                    objMembers.MobilePhone = (dt.Rows[0]["MobilePhone"] != DBNull.Value ? dt.Rows[0]["MobilePhone"].ToString() : null);
                    objMembers.IsApproved = Convert.ToBoolean(dt.Rows[0]["IsApproved"]);
                    objMembers.IsLockedOut = Convert.ToBoolean(dt.Rows[0]["IsLockedOut"]);
                    objMembers.IsActivated = Convert.ToBoolean(dt.Rows[0]["IsActivated"]);
                    objMembers.DateActivated = (dt.Rows[0]["DateActivated"] != DBNull.Value ?Convert.ToDateTime(dt.Rows[0]["DateActivated"]) : DateTime.MinValue);
                    objMembers.MembershipTypeId = Convert.ToInt64(dt.Rows[0]["MembershipTypeId"]);
                    objMembers.MembershipType = dt.Rows[0]["MembershipType"].ToString();
                    objMembers.IsVolunteer = Convert.ToBoolean(dt.Rows[0]["IsVolunteer"]);
                    objMembers.IsTeluguorigin = Convert.ToBoolean(dt.Rows[0]["IsTeluguorigin"]);
                    objMembers.Comments = (dt.Rows[0]["Comments"] != DBNull.Value ? dt.Rows[0]["Comments"].ToString() : null);
                    objMembers.ReferredBy = (dt.Rows[0]["ReferredBy"] != DBNull.Value ? dt.Rows[0]["ReferredBy"].ToString() : null);
                    objMembers.MobilePhone = (dt.Rows[0]["MobilePhone"] != DBNull.Value ? dt.Rows[0]["MobilePhone"].ToString() : null);
                    objMembers.InsertedTime = Convert.ToDateTime(dt.Rows[0]["InsertedTime"]);
                    objMembers.UpdatedTime = Convert.ToDateTime(dt.Rows[0]["UpdatedTime"]);
                    objMembers.Fax = (dt.Rows[0]["Fax"] != DBNull.Value ? dt.Rows[0]["Fax"].ToString() : null);
                    objMembers.WebsiteAddress = (dt.Rows[0]["WebsiteAddress"] != DBNull.Value ? dt.Rows[0]["WebsiteAddress"].ToString() : null);
                    objMembers.Address2 = (dt.Rows[0]["Address2"] != DBNull.Value ? dt.Rows[0]["Address2"].ToString() : null);
            }

            return objMembers;
        }

        public List<Entities.Members> GetMembersList(ref int status)
        {
            DataTable dt = _Members.GetMembersList(ref status);
            List<Entities.Members> lstMembers = new List<Entities.Members>();

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Entities.Members objMembers = new Entities.Members();

                    objMembers.RId = Convert.ToInt64(dr["Rid"]);
                    objMembers.MemberId = Convert.ToInt64(dr["MemberId"]);
                    objMembers.Title = (dr["Title"] != DBNull.Value ? dr["Title"].ToString() : null);
                    objMembers.Email = dr["Email"].ToString();
                    objMembers.FirstName = dr["FirstName"].ToString();
                    objMembers.LastName = dr["LastName"].ToString();
                    objMembers.ProfileImage = (dr["ProfileImage"] != DBNull.Value ? dr["ProfileImage"].ToString() : null);
                    objMembers.Occupation = (dr["Occupation"] != DBNull.Value ? dr["Occupation"].ToString() : null);
                    objMembers.SpouseFirstName = (dr["SpouseFirstName"] != DBNull.Value ? dr["SpouseFirstName"].ToString() : null);
                    objMembers.SpouseLastName = (dr["SpouseLastName"] != DBNull.Value ? dr["SpouseLastName"].ToString() : null);
                    objMembers.SpouseOccupation = (dr["SpouseOccupation"] != DBNull.Value ? dr["SpouseOccupation"].ToString() : null);
                    objMembers.SpouseEmail = (dr["SpouseEmail"] != DBNull.Value ? dr["SpouseEmail"].ToString() : null);
                    objMembers.SpouseCell = (dr["SpouseCell"] != DBNull.Value ? dr["SpouseCell"].ToString() : null);
                    objMembers.Address = (dr["Address"] != DBNull.Value ? dr["Address"].ToString() : null);
                    objMembers.City = (dr["City"] != DBNull.Value ? dr["City"].ToString() : null);
                    objMembers.State = (dr["State"] != DBNull.Value ? dr["State"].ToString() : null);
                    objMembers.ZipCode = (dr["ZipCode"] != DBNull.Value ? dr["ZipCode"].ToString() : null);
                    objMembers.HomePhone = (dr["HomePhone"] != DBNull.Value ? dr["HomePhone"].ToString() : null);
                    objMembers.MobilePhone = (dr["MobilePhone"] != DBNull.Value ? dr["MobilePhone"].ToString() : null);
                    objMembers.IsApproved = Convert.ToBoolean(dr["IsApproved"]);
                    objMembers.IsLockedOut = Convert.ToBoolean(dr["IsLockedOut"]);
                    objMembers.IsActivated = Convert.ToBoolean(dr["IsActivated"]);
                    objMembers.DateActivated = (dr["DateActivated"] != DBNull.Value ? Convert.ToDateTime(dr["DateActivated"]) : DateTime.MinValue);
                    objMembers.MembershipTypeId = Convert.ToInt64(dr["MembershipTypeId"]);
                    objMembers.MembershipType = dr["MembershipType"].ToString();
                    objMembers.IsVolunteer = Convert.ToBoolean(dr["IsVolunteer"]);
                    objMembers.IsTeluguorigin = Convert.ToBoolean(dr["IsTeluguorigin"]);
                    objMembers.Comments = (dr["Comments"] != DBNull.Value ? dr["Comments"].ToString() : null);
                    objMembers.ReferredBy = (dr["ReferredBy"] != DBNull.Value ? dr["ReferredBy"].ToString() : null);
                    objMembers.MobilePhone = (dr["MobilePhone"] != DBNull.Value ? dr["MobilePhone"].ToString() : null);
                    objMembers.InsertedTime = Convert.ToDateTime(dr["InsertedTime"]);
                    objMembers.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"]);
                    objMembers.Fax = (dr["Fax"] != DBNull.Value ? dr["Fax"].ToString() : null);
                    objMembers.WebsiteAddress = (dr["WebsiteAddress"] != DBNull.Value ? dr["WebsiteAddress"].ToString() : null);
                    objMembers.Address2 = (dr["Address2"] != DBNull.Value ? dr["Address2"].ToString() : null);
                    lstMembers.Add(objMembers);
                }
            }

            return lstMembers;
        }
public List<Entities.Members> MemberLogsGetList(Int64 MemberId, ref int status)
        {
            DataTable dt = _Members.MemberLogsGetList(MemberId,ref status);
            List<Entities.Members> lstMembers = new List<Entities.Members>();

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Entities.Members objMembers = new Entities.Members();

                    
                    objMembers.MemberId = Convert.ToInt64(dr["MemberId"]);
                    objMembers.Email = dr["Email"].ToString();
                    objMembers.FirstName = dr["FirstName"].ToString();
                    objMembers.LastName = dr["LastName"].ToString();
                    objMembers.Address = (dr["Address"] != DBNull.Value ? dr["Address"].ToString() : null);
                    objMembers.City = (dr["City"] != DBNull.Value ? dr["City"].ToString() : null);
                    objMembers.State = (dr["State"] != DBNull.Value ? dr["State"].ToString() : null);
                    objMembers.ZipCode = (dr["ZipCode"] != DBNull.Value ? dr["ZipCode"].ToString() : null);
                    objMembers.MobilePhone = (dr["MobilePhone"] != DBNull.Value ? dr["MobilePhone"].ToString() : null);
                    //objMembers.IsApproved = Convert.ToBoolean(dr["IsApproved"]);
                    objMembers.MembershipTypeId = Convert.ToInt64(dr["MembershipTypeId"]);
                    objMembers.MembershipType = dr["MembershipType"].ToString();
                    objMembers.TransactionId = (dr["TransactionId"] != DBNull.Value ? dr["TransactionId"].ToString() : null);
                    objMembers.Address2 = (dr["Address2"] != DBNull.Value ? dr["Address2"].ToString() : null);
                    objMembers.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"]);
                    objMembers.Amount = (dr["Amount"] != DBNull.Value ? Convert.ToDecimal(dr["Amount"]) : 0);
                    objMembers.AdminComment = (dr["AdminComment"] != DBNull.Value ? dr["AdminComment"].ToString() : null);
                    objMembers.UserComment = (dr["UserComment"] != DBNull.Value ? dr["UserComment"].ToString() : null);
                    objMembers.PaymentStatus = (dr["PaymentStatus"] != DBNull.Value ? dr["PaymentStatus"].ToString() : null);
                    objMembers.PaymentMethod = (dr["PaymentMethod"] != DBNull.Value ? dr["PaymentMethod"].ToString() : null);
                    objMembers.UpdatedBy = (dr["UpdatedBy"] != DBNull.Value ? dr["UpdatedBy"].ToString() : null);

                    lstMembers.Add(objMembers);
                }
            }

            return lstMembers;
        }

        public Entities.Members GetMembersFullDetailsById(Int64 MembersId, ref int status)
        {
            DataSet ds1 = _Members.GetMembersFullDetailsById(MembersId, ref status);

            DataTable dt_Members = ds1.Tables[0];
            DataTable dt_ChildrenInfo = ds1.Tables[1];
            DataTable dt_MembershipOrder = ds1.Tables[2];
             

            Entities.Members objMembers = new Entities.Members();
            List<Entities.ChildrenInfo> lstChildrenInfo = new List<Entities.ChildrenInfo>();
            List<Entities.MembershipOrders> lstMembershipOrder = new List<Entities.MembershipOrders>();

            if (dt_Members.Rows.Count ==1)
            {
               
                objMembers.MemberId = Convert.ToInt64(dt_Members.Rows[0]["MemberId"]);
                objMembers.ChapterId = (dt_Members.Rows[0]["ChapterId"] != DBNull.Value ? Convert.ToInt64(dt_Members.Rows[0]["ChapterId"]) : 0);
                objMembers.Title = (dt_Members.Rows[0]["Title"] != DBNull.Value ? dt_Members.Rows[0]["Title"].ToString() : null);
                objMembers.Email = dt_Members.Rows[0]["Email"].ToString();
                objMembers.FirstName = dt_Members.Rows[0]["FirstName"].ToString();
                objMembers.LastName = dt_Members.Rows[0]["LastName"].ToString();
                objMembers.ProfileImage = (dt_Members.Rows[0]["ProfileImage"] != DBNull.Value ? dt_Members.Rows[0]["ProfileImage"].ToString() : null);
                objMembers.Occupation = (dt_Members.Rows[0]["Occupation"] != DBNull.Value ? dt_Members.Rows[0]["Occupation"].ToString() : null);
                objMembers.MemberAge = (dt_Members.Rows[0]["MemberAge"] != DBNull.Value ? dt_Members.Rows[0]["MemberAge"].ToString() : null);
                objMembers.MemberSkils = (dt_Members.Rows[0]["Occupation"] != DBNull.Value ? dt_Members.Rows[0]["MemberSkils"].ToString() : null);
                objMembers.SpouseSkils = (dt_Members.Rows[0]["SpouseSkils"] != DBNull.Value ? dt_Members.Rows[0]["SpouseSkils"].ToString() : null);
                objMembers.SpouseFirstName = (dt_Members.Rows[0]["SpouseFirstName"] != DBNull.Value ? dt_Members.Rows[0]["SpouseFirstName"].ToString() : null);
                objMembers.SpouseLastName = (dt_Members.Rows[0]["SpouseLastName"] != DBNull.Value ? dt_Members.Rows[0]["SpouseLastName"].ToString() : null);
                objMembers.SpouseOccupation = (dt_Members.Rows[0]["SpouseOccupation"] != DBNull.Value ? dt_Members.Rows[0]["SpouseOccupation"].ToString() : null);
                objMembers.SpouseEmail = (dt_Members.Rows[0]["SpouseEmail"] != DBNull.Value ? dt_Members.Rows[0]["SpouseEmail"].ToString() : null);
                objMembers.SpouseCell = (dt_Members.Rows[0]["SpouseCell"] != DBNull.Value ? dt_Members.Rows[0]["SpouseCell"].ToString() : null);
                objMembers.Address = (dt_Members.Rows[0]["Address"] != DBNull.Value ? dt_Members.Rows[0]["Address"].ToString() : null);
                objMembers.City = (dt_Members.Rows[0]["City"] != DBNull.Value ? dt_Members.Rows[0]["City"].ToString() : null);
                objMembers.State = (dt_Members.Rows[0]["State"] != DBNull.Value ? dt_Members.Rows[0]["State"].ToString() : null);
                objMembers.ZipCode = (dt_Members.Rows[0]["ZipCode"] != DBNull.Value ? dt_Members.Rows[0]["ZipCode"].ToString() : null);
                objMembers.HomePhone = (dt_Members.Rows[0]["HomePhone"] != DBNull.Value ? dt_Members.Rows[0]["HomePhone"].ToString() : null);
                objMembers.MobilePhone = (dt_Members.Rows[0]["MobilePhone"] != DBNull.Value ? dt_Members.Rows[0]["MobilePhone"].ToString() : null);
                objMembers.IsApproved = Convert.ToBoolean(dt_Members.Rows[0]["IsApproved"]);
                objMembers.IsLockedOut = (dt_Members.Rows[0]["IsLockedOut"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsLockedOut"]) : false);
                objMembers.IsActivated = (dt_Members.Rows[0]["IsActivated"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsActivated"]) : false);
                objMembers.DateActivated = (dt_Members.Rows[0]["DateActivated"] != DBNull.Value ? Convert.ToDateTime(dt_Members.Rows[0]["DateActivated"]) : DateTime.MinValue);
                objMembers.MembershipTypeId = Convert.ToInt64(dt_Members.Rows[0]["MembershipTypeId"]);
                objMembers.MembershipType = dt_Members.Rows[0]["MembershipType"].ToString();
                objMembers.IsVolunteer = Convert.ToBoolean(dt_Members.Rows[0]["IsVolunteer"]);
                objMembers.IsTeluguorigin = (dt_Members.Rows[0]["IsTeluguorigin"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsTeluguorigin"]) : false);
                objMembers.Comments = (dt_Members.Rows[0]["Comments"] != DBNull.Value ? dt_Members.Rows[0]["Comments"].ToString() : null);
                objMembers.ReferredBy = (dt_Members.Rows[0]["ReferredBy"] != DBNull.Value ? dt_Members.Rows[0]["ReferredBy"].ToString() : null);
                objMembers.MobilePhone = (dt_Members.Rows[0]["MobilePhone"] != DBNull.Value ? dt_Members.Rows[0]["MobilePhone"].ToString() : null);
                objMembers.InsertedTime = Convert.ToDateTime(dt_Members.Rows[0]["InsertedTime"]);
                objMembers.UpdatedTime = Convert.ToDateTime(dt_Members.Rows[0]["UpdatedTime"]);
                objMembers.Fax = (dt_Members.Rows[0]["Fax"] != DBNull.Value ? dt_Members.Rows[0]["Fax"].ToString() : null);
                objMembers.WebsiteAddress = (dt_Members.Rows[0]["WebsiteAddress"] != DBNull.Value ? dt_Members.Rows[0]["WebsiteAddress"].ToString() : null);
                objMembers.Address2 = (dt_Members.Rows[0]["Address2"] != DBNull.Value ? dt_Members.Rows[0]["Address2"].ToString() : null);
           
            
            }

            if (dt_ChildrenInfo.Rows.Count != 0)
            {               

                foreach (DataRow dr in dt_ChildrenInfo.Rows)
                {
                    Entities.ChildrenInfo objChildrenInfo = new Entities.ChildrenInfo();

                    objChildrenInfo.ChildrenInfoId = Convert.ToInt64(dr["ChildrenInfoId"]);
                    objChildrenInfo.MemberId = Convert.ToInt64(dr["MemberId"]);
                    objChildrenInfo.FirstName = dr["FirstName"].ToString();
                    objChildrenInfo.LastName = dr["LastName"].ToString();
                    objChildrenInfo.Age = (dr["Age"] != DBNull.Value ? Convert.ToInt32(dr["Age"].ToString()) : 0);
                    objChildrenInfo.Relationship = dr["Relationship"].ToString();

                    lstChildrenInfo.Add(objChildrenInfo);
                }
            }

            objMembers.lstChildrenInfo = lstChildrenInfo;

            if (dt_MembershipOrder.Rows.Count != 0)
            {

                foreach (DataRow dr in dt_MembershipOrder.Rows)
                {
                    Entities.MembershipOrders objMembershipOrder = new Entities.MembershipOrders();

                    objMembershipOrder.MembershipOrderId = Convert.ToInt64(dr["MembershipOrderId"]);
                    objMembershipOrder.MemberId = (dr["MemberId"] != DBNull.Value ? Convert.ToInt64(dr["MemberId"]) : 0);
                    objMembershipOrder.MembershipTypeId = (dr["MembershipTypeId"] != DBNull.Value ? Convert.ToInt64(dr["MembershipTypeId"]) : 0);
                    objMembershipOrder.Amount = (dr["Amount"] != DBNull.Value ? Convert.ToDecimal(dr["Amount"]) : 0);
                    objMembershipOrder.TransactionId = (dr["TransactionId"] != DBNull.Value ?dr["TransactionId"].ToString() : null);
                    objMembershipOrder.MembershipTypeId = (dr["MembershipTypeId"] != DBNull.Value ? Convert.ToInt64(dr["MembershipTypeId"]) : 0);
                    objMembershipOrder.PaymentStatusId = (dr["PaymentStatusId"] != DBNull.Value ? Convert.ToInt64(dr["PaymentStatusId"]) : 0);
                    objMembershipOrder.PaymentMethodId = (dr["MembershipTypeId"] != DBNull.Value ? Convert.ToInt64(dr["MembershipTypeId"]) : 0);
                    objMembershipOrder.MembershipTypeId = (dr["PaymentMethodId"] != DBNull.Value ? Convert.ToInt64(dr["PaymentMethodId"]) : 0);
                    objMembershipOrder.PaymentBy = (dr["PaymentBy"] != DBNull.Value ? dr["PaymentBy"].ToString() : null);
                    objMembershipOrder.AdminComment = (dr["AdminComment"] != DBNull.Value ? dr["AdminComment"].ToString() : null);
                    objMembershipOrder.UserComment = (dr["UserComment"] != DBNull.Value ? dr["UserComment"].ToString() : null);
                    objMembershipOrder.OrderDate = (dr["OrderDate"] != DBNull.Value ?Convert.ToDateTime(dr["OrderDate"]) : DateTime.MinValue);
                    objMembershipOrder.ExpiryDate = (dr["ExpiryDate"] != DBNull.Value ? Convert.ToDateTime(dr["ExpiryDate"]) : DateTime.MinValue);
                    objMembershipOrder.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"]);
                    objMembershipOrder.UpdatedBy = dr["UpdatedBy"].ToString();

                    lstMembershipOrder.Add(objMembershipOrder);
                }
            }

            objMembers.lstMembershipOrder = lstMembershipOrder;

            return objMembers;
        }

        public Entities.Members GetMembersFullDetailsByEmail(string Email, ref int status)
        {
            DataSet ds1 = _Members.GetMembersFullDetailsByEmail(Email, ref status);

            DataTable dt_Members = ds1.Tables[0];
            DataTable dt_ChildrenInfo = ds1.Tables[1];
            DataTable dt_MembershipOrder = ds1.Tables[2];


            Entities.Members objMembers = new Entities.Members();
            List<Entities.ChildrenInfo> lstChildrenInfo = new List<Entities.ChildrenInfo>();
            List<Entities.MembershipOrders> lstMembershipOrder = new List<Entities.MembershipOrders>();

            if (dt_Members.Rows.Count == 1)
            {

                objMembers.MemberId = Convert.ToInt64(dt_Members.Rows[0]["MemberId"]);
                objMembers.ChapterId = (dt_Members.Rows[0]["ChapterId"] != DBNull.Value ? Convert.ToInt64(dt_Members.Rows[0]["ChapterId"]) : 0);
                objMembers.Title = (dt_Members.Rows[0]["Title"] != DBNull.Value ? dt_Members.Rows[0]["Title"].ToString() : null);
                objMembers.Email = dt_Members.Rows[0]["Email"].ToString();
                objMembers.FirstName = dt_Members.Rows[0]["FirstName"].ToString();
                objMembers.LastName = dt_Members.Rows[0]["LastName"].ToString();
                objMembers.ProfileImage = (dt_Members.Rows[0]["ProfileImage"] != DBNull.Value ? dt_Members.Rows[0]["ProfileImage"].ToString() : null);
                objMembers.Occupation = (dt_Members.Rows[0]["Occupation"] != DBNull.Value ? dt_Members.Rows[0]["Occupation"].ToString() : null);
                objMembers.MemberAge = (dt_Members.Rows[0]["MemberAge"] != DBNull.Value ? dt_Members.Rows[0]["MemberAge"].ToString() : null);
                objMembers.MemberSkils = (dt_Members.Rows[0]["Occupation"] != DBNull.Value ? dt_Members.Rows[0]["MemberSkils"].ToString() : null);
                objMembers.SpouseSkils = (dt_Members.Rows[0]["SpouseSkils"] != DBNull.Value ? dt_Members.Rows[0]["SpouseSkils"].ToString() : null);
                objMembers.SpouseFirstName = (dt_Members.Rows[0]["SpouseFirstName"] != DBNull.Value ? dt_Members.Rows[0]["SpouseFirstName"].ToString() : null);
                objMembers.SpouseLastName = (dt_Members.Rows[0]["SpouseLastName"] != DBNull.Value ? dt_Members.Rows[0]["SpouseLastName"].ToString() : null);
                objMembers.SpouseOccupation = (dt_Members.Rows[0]["SpouseOccupation"] != DBNull.Value ? dt_Members.Rows[0]["SpouseOccupation"].ToString() : null);
                objMembers.SpouseEmail = (dt_Members.Rows[0]["SpouseEmail"] != DBNull.Value ? dt_Members.Rows[0]["SpouseEmail"].ToString() : null);
                objMembers.SpouseCell = (dt_Members.Rows[0]["SpouseCell"] != DBNull.Value ? dt_Members.Rows[0]["SpouseCell"].ToString() : null);
                objMembers.Address = (dt_Members.Rows[0]["Address"] != DBNull.Value ? dt_Members.Rows[0]["Address"].ToString() : null);
                objMembers.City = (dt_Members.Rows[0]["City"] != DBNull.Value ? dt_Members.Rows[0]["City"].ToString() : null);
                objMembers.State = (dt_Members.Rows[0]["State"] != DBNull.Value ? dt_Members.Rows[0]["State"].ToString() : null);
                objMembers.ZipCode = (dt_Members.Rows[0]["ZipCode"] != DBNull.Value ? dt_Members.Rows[0]["ZipCode"].ToString() : null);
                objMembers.HomePhone = (dt_Members.Rows[0]["HomePhone"] != DBNull.Value ? dt_Members.Rows[0]["HomePhone"].ToString() : null);
                objMembers.MobilePhone = (dt_Members.Rows[0]["MobilePhone"] != DBNull.Value ? dt_Members.Rows[0]["MobilePhone"].ToString() : null);
                objMembers.IsApproved = Convert.ToBoolean(dt_Members.Rows[0]["IsApproved"]);
                objMembers.IsLockedOut = (dt_Members.Rows[0]["IsLockedOut"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsLockedOut"]) : false);
                objMembers.IsActivated = (dt_Members.Rows[0]["IsActivated"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsActivated"]) : false);
                objMembers.DateActivated = (dt_Members.Rows[0]["DateActivated"] != DBNull.Value ? Convert.ToDateTime(dt_Members.Rows[0]["DateActivated"]) : DateTime.MinValue);
                objMembers.MembershipTypeId = Convert.ToInt64(dt_Members.Rows[0]["MembershipTypeId"]);
                objMembers.MembershipType = dt_Members.Rows[0]["MembershipType"].ToString();
                objMembers.IsVolunteer = Convert.ToBoolean(dt_Members.Rows[0]["IsVolunteer"]);
                objMembers.IsTeluguorigin = (dt_Members.Rows[0]["IsTeluguorigin"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsTeluguorigin"]) : false);
                objMembers.Comments = (dt_Members.Rows[0]["Comments"] != DBNull.Value ? dt_Members.Rows[0]["Comments"].ToString() : null);
                objMembers.ReferredBy = (dt_Members.Rows[0]["ReferredBy"] != DBNull.Value ? dt_Members.Rows[0]["ReferredBy"].ToString() : null);
                objMembers.MobilePhone = (dt_Members.Rows[0]["MobilePhone"] != DBNull.Value ? dt_Members.Rows[0]["MobilePhone"].ToString() : null);
                objMembers.InsertedTime = Convert.ToDateTime(dt_Members.Rows[0]["InsertedTime"]);
                objMembers.UpdatedTime = Convert.ToDateTime(dt_Members.Rows[0]["UpdatedTime"]);
                objMembers.Fax = (dt_Members.Rows[0]["Fax"] != DBNull.Value ? dt_Members.Rows[0]["Fax"].ToString() : null);
                objMembers.WebsiteAddress = (dt_Members.Rows[0]["WebsiteAddress"] != DBNull.Value ? dt_Members.Rows[0]["WebsiteAddress"].ToString() : null);
                objMembers.Address2 = (dt_Members.Rows[0]["Address2"] != DBNull.Value ? dt_Members.Rows[0]["Address2"].ToString() : null);


            }

            if (dt_ChildrenInfo.Rows.Count != 0)
            {

                foreach (DataRow dr in dt_ChildrenInfo.Rows)
                {
                    Entities.ChildrenInfo objChildrenInfo = new Entities.ChildrenInfo();

                    objChildrenInfo.ChildrenInfoId = Convert.ToInt64(dr["ChildrenInfoId"]);
                    objChildrenInfo.MemberId = Convert.ToInt64(dr["MemberId"]);
                    objChildrenInfo.FirstName = dr["FirstName"].ToString();
                    objChildrenInfo.LastName = dr["LastName"].ToString();
                    objChildrenInfo.Age = (dr["Age"] != DBNull.Value ? Convert.ToInt32(dr["Age"].ToString()) : 0);
                    objChildrenInfo.Relationship = dr["Relationship"].ToString();

                    lstChildrenInfo.Add(objChildrenInfo);
                }
            }

            objMembers.lstChildrenInfo = lstChildrenInfo;

            if (dt_MembershipOrder.Rows.Count != 0)
            {

                foreach (DataRow dr in dt_MembershipOrder.Rows)
                {
                    Entities.MembershipOrders objMembershipOrder = new Entities.MembershipOrders();

                    objMembershipOrder.MembershipOrderId = Convert.ToInt64(dr["MembershipOrderId"]);
                    objMembershipOrder.MemberId = (dr["MemberId"] != DBNull.Value ? Convert.ToInt64(dr["MemberId"]) : 0);
                    objMembershipOrder.MembershipTypeId = (dr["MembershipTypeId"] != DBNull.Value ? Convert.ToInt64(dr["MembershipTypeId"]) : 0);
                    objMembershipOrder.Amount = (dr["Amount"] != DBNull.Value ? Convert.ToDecimal(dr["Amount"]) : 0);
                    objMembershipOrder.TransactionId = (dr["TransactionId"] != DBNull.Value ? dr["TransactionId"].ToString() : null);
                    objMembershipOrder.MembershipTypeId = (dr["MembershipTypeId"] != DBNull.Value ? Convert.ToInt64(dr["MembershipTypeId"]) : 0);
                    objMembershipOrder.PaymentStatusId = (dr["PaymentStatusId"] != DBNull.Value ? Convert.ToInt64(dr["PaymentStatusId"]) : 0);
                    objMembershipOrder.PaymentMethodId = (dr["MembershipTypeId"] != DBNull.Value ? Convert.ToInt64(dr["MembershipTypeId"]) : 0);
                    objMembershipOrder.MembershipTypeId = (dr["PaymentMethodId"] != DBNull.Value ? Convert.ToInt64(dr["PaymentMethodId"]) : 0);
                    objMembershipOrder.PaymentBy = (dr["PaymentBy"] != DBNull.Value ? dr["PaymentBy"].ToString() : null);
                    objMembershipOrder.AdminComment = (dr["AdminComment"] != DBNull.Value ? dr["AdminComment"].ToString() : null);
                    objMembershipOrder.UserComment = (dr["UserComment"] != DBNull.Value ? dr["UserComment"].ToString() : null);
                    objMembershipOrder.OrderDate = (dr["OrderDate"] != DBNull.Value ? Convert.ToDateTime(dr["OrderDate"]) : DateTime.MinValue);
                    objMembershipOrder.ExpiryDate = (dr["ExpiryDate"] != DBNull.Value ? Convert.ToDateTime(dr["ExpiryDate"]) : DateTime.MinValue);
                    objMembershipOrder.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"]);
                    objMembershipOrder.UpdatedBy = dr["UpdatedBy"].ToString();

                    lstMembershipOrder.Add(objMembershipOrder);
                }
            }

            objMembers.lstMembershipOrder = lstMembershipOrder;

            return objMembers;
        }

        public Entities.Members GetMemberFullDetailsById(Int64 MembersId, ref int status)
        {
            DataSet ds0 = _Members1.GetMemberFullDetailsById(MembersId, ref status);

            DataTable dt_Members = ds0.Tables[0];
            DataTable dt_ChildrenInfo = ds0.Tables[1];
            DataTable dt_MembershipOrder = ds0.Tables[2];
            DataTable dt_MembersInfo = ds0.Tables[3];



            Entities.Members objMembers = new Entities.Members();
            List<Entities.ChildrenInfo> lstChildrenInfo = new List<Entities.ChildrenInfo>();
            List<Entities.MembershipOrders> lstMembershipOrder = new List<Entities.MembershipOrders>();

            if (dt_Members.Rows.Count == 1)
            {

                objMembers.MemberId = Convert.ToInt64(dt_Members.Rows[0]["MemberId"]);
                objMembers.ChapterId = (dt_Members.Rows[0]["ChapterId"] != DBNull.Value ? Convert.ToInt64(dt_Members.Rows[0]["ChapterId"]) : 0);
                objMembers.Title = (dt_Members.Rows[0]["Title"] != DBNull.Value ? dt_Members.Rows[0]["Title"].ToString() : null);
                objMembers.Email = dt_Members.Rows[0]["Email"].ToString();
                objMembers.FirstName = dt_Members.Rows[0]["FirstName"].ToString();
                objMembers.LastName = dt_Members.Rows[0]["LastName"].ToString();
                objMembers.ProfileImage = (dt_Members.Rows[0]["ProfileImage"] != DBNull.Value ? dt_Members.Rows[0]["ProfileImage"].ToString() : null);
                objMembers.Occupation = (dt_Members.Rows[0]["Occupation"] != DBNull.Value ? dt_Members.Rows[0]["Occupation"].ToString() : null);
                objMembers.MemberAge = (dt_Members.Rows[0]["MemberAge"] != DBNull.Value ? dt_Members.Rows[0]["MemberAge"].ToString() : null);
                objMembers.MemberSkils = (dt_Members.Rows[0]["MemberSkils"] != DBNull.Value ? dt_Members.Rows[0]["MemberSkils"].ToString() : null);
                objMembers.SpouseSkils = (dt_Members.Rows[0]["SpouseSkils"] != DBNull.Value ? dt_Members.Rows[0]["SpouseSkils"].ToString() : null);
                objMembers.SpouseFirstName = (dt_Members.Rows[0]["SpouseFirstName"] != DBNull.Value ? dt_Members.Rows[0]["SpouseFirstName"].ToString() : null);
                objMembers.SpouseLastName = (dt_Members.Rows[0]["SpouseLastName"] != DBNull.Value ? dt_Members.Rows[0]["SpouseLastName"].ToString() : null);
                objMembers.SpouseOccupation = (dt_Members.Rows[0]["SpouseOccupation"] != DBNull.Value ? dt_Members.Rows[0]["SpouseOccupation"].ToString() : null);
                objMembers.SpouseEmail = (dt_Members.Rows[0]["SpouseEmail"] != DBNull.Value ? dt_Members.Rows[0]["SpouseEmail"].ToString() : null);
                objMembers.SpouseCell = (dt_Members.Rows[0]["SpouseCell"] != DBNull.Value ? dt_Members.Rows[0]["SpouseCell"].ToString() : null);
                objMembers.Address = (dt_Members.Rows[0]["Address"] != DBNull.Value ? dt_Members.Rows[0]["Address"].ToString() : null);
                objMembers.City = (dt_Members.Rows[0]["City"] != DBNull.Value ? dt_Members.Rows[0]["City"].ToString() : null);
                objMembers.State = (dt_Members.Rows[0]["State"] != DBNull.Value ? dt_Members.Rows[0]["State"].ToString() : null);
                objMembers.ZipCode = (dt_Members.Rows[0]["ZipCode"] != DBNull.Value ? dt_Members.Rows[0]["ZipCode"].ToString() : null);
                objMembers.HomePhone = (dt_Members.Rows[0]["HomePhone"] != DBNull.Value ? dt_Members.Rows[0]["HomePhone"].ToString() : null);
                objMembers.MobilePhone = (dt_Members.Rows[0]["MobilePhone"] != DBNull.Value ? dt_Members.Rows[0]["MobilePhone"].ToString() : null);
                objMembers.IsApproved = Convert.ToBoolean(dt_Members.Rows[0]["IsApproved"]);
                objMembers.IsLockedOut = (dt_Members.Rows[0]["IsLockedOut"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsLockedOut"]) : false);
                objMembers.MemberAmount = (dt_Members.Rows[0]["MemberAmount"] != DBNull.Value ? Convert.ToDecimal(dt_Members.Rows[0]["MemberAmount"]) : 0);
                objMembers.IsActivated = (dt_Members.Rows[0]["IsLockedOut"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsLockedOut"]) : false);
                objMembers.DateActivated = (dt_Members.Rows[0]["DateActivated"] != DBNull.Value ? Convert.ToDateTime(dt_Members.Rows[0]["DateActivated"]) : DateTime.MinValue);
                objMembers.MembershipTypeId = Convert.ToInt64(dt_Members.Rows[0]["MembershipTypeId"]);
                objMembers.MembershipType = dt_Members.Rows[0]["MembershipType"].ToString();
                objMembers.IsVolunteer = (dt_Members.Rows[0]["IsVolunteer"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsVolunteer"]) : false);
                objMembers.IsTeluguorigin = (dt_Members.Rows[0]["IsTeluguorigin"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsTeluguorigin"]) : false);
                objMembers.Comments = (dt_Members.Rows[0]["Comments"] != DBNull.Value ? dt_Members.Rows[0]["Comments"].ToString() : null);
                objMembers.ReferredBy = (dt_Members.Rows[0]["ReferredBy"] != DBNull.Value ? dt_Members.Rows[0]["ReferredBy"].ToString() : null);
                objMembers.MobilePhone = (dt_Members.Rows[0]["MobilePhone"] != DBNull.Value ? dt_Members.Rows[0]["MobilePhone"].ToString() : null);
                objMembers.InsertedTime = Convert.ToDateTime(dt_Members.Rows[0]["InsertedTime"]);
                objMembers.UpdatedTime = Convert.ToDateTime(dt_Members.Rows[0]["UpdatedTime"]);
                objMembers.Fax = (dt_Members.Rows[0]["Fax"] != DBNull.Value ? dt_Members.Rows[0]["Fax"].ToString() : null);
                objMembers.WebsiteAddress = (dt_Members.Rows[0]["WebsiteAddress"] != DBNull.Value ? dt_Members.Rows[0]["WebsiteAddress"].ToString() : null);
                objMembers.Address2 = (dt_Members.Rows[0]["Address2"] != DBNull.Value ? dt_Members.Rows[0]["Address2"].ToString() : null);
                objMembers.ReceiptUrl = (dt_Members.Rows[0]["ReceiptUrl"] != DBNull.Value ? dt_Members.Rows[0]["ReceiptUrl"].ToString() : null);
                objMembers.ZellePartnerName = (dt_Members.Rows[0]["ZellePartnerName"] != DBNull.Value ? dt_Members.Rows[0]["ZellePartnerName"].ToString() : null);
                objMembers.ChequeHolderName = (dt_Members.Rows[0]["ChequeHolderName"] != DBNull.Value ? dt_Members.Rows[0]["ChequeHolderName"].ToString() : null);
                objMembers.Zelle = (dt_Members.Rows[0]["Zelle"] != DBNull.Value ? dt_Members.Rows[0]["Zelle"].ToString() : null);

                objMembers.ChapterName = (dt_Members.Rows[0]["ChapterName"] != DBNull.Value ? dt_Members.Rows[0]["ChapterName"].ToString() : null);

                objMembers.MaritalStatus = (dt_Members.Rows[0]["MaritalStatus"] != DBNull.Value ? dt_Members.Rows[0]["MaritalStatus"].ToString() : null);

                objMembers.gender = (dt_Members.Rows[0]["gender"] != DBNull.Value ? dt_Members.Rows[0]["gender"].ToString() : null);

            }

            if (dt_ChildrenInfo.Rows.Count != 0)
            {

                foreach (DataRow dr in dt_ChildrenInfo.Rows)
                {
                    Entities.ChildrenInfo objChildrenInfo = new Entities.ChildrenInfo();

                    objChildrenInfo.ChildrenInfoId = Convert.ToInt64(dr["ChildrenInfoId"]);
                    objChildrenInfo.MemberId = Convert.ToInt64(dr["MemberId"]);
                    objChildrenInfo.FirstName = dr["FirstName"].ToString();
                    objChildrenInfo.LastName = dr["LastName"].ToString();
                    objChildrenInfo.Age = (dr["Age"] != DBNull.Value ?Convert.ToInt32(dr["Age"].ToString()) : 0);
                    objChildrenInfo.Relationship = dr["Relationship"].ToString();
                    objChildrenInfo.Email = dr["Email"].ToString();

                    lstChildrenInfo.Add(objChildrenInfo);
                }
            }

            objMembers.lstChildrenInfo = lstChildrenInfo;

            if (dt_MembershipOrder.Rows.Count == 1)
            {
                objMembers.objMembershipOrder.MembershipOrderId = Convert.ToInt64(dt_MembershipOrder.Rows[0]["MembershipOrderId"]);
                objMembers.objMembershipOrder.MemberId = (dt_MembershipOrder.Rows[0]["MemberId"] != DBNull.Value ? Convert.ToInt64(dt_MembershipOrder.Rows[0]["MemberId"]) : 0);
                objMembers.objMembershipOrder.MembershipTypeId = (dt_MembershipOrder.Rows[0]["MembershipTypeId"] != DBNull.Value ? Convert.ToInt64(dt_MembershipOrder.Rows[0]["MembershipTypeId"]) : 0);
                objMembers.objMembershipOrder.Amount = (dt_MembershipOrder.Rows[0]["Amount"] != DBNull.Value ? Convert.ToDecimal(dt_MembershipOrder.Rows[0]["Amount"]) : 0);
                objMembers.objMembershipOrder.TransactionId = (dt_MembershipOrder.Rows[0]["TransactionId"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["TransactionId"].ToString() : null);
                objMembers.objMembershipOrder.PaymentStatusId = (dt_MembershipOrder.Rows[0]["PaymentStatusId"] != DBNull.Value ? Convert.ToInt64(dt_MembershipOrder.Rows[0]["PaymentStatusId"]) : 0);
                objMembers.objMembershipOrder.PaymentMethodId = (dt_MembershipOrder.Rows[0]["PaymentMethodId"] != DBNull.Value ? Convert.ToInt64(dt_MembershipOrder.Rows[0]["PaymentMethodId"]) : 0);
                objMembers.objMembershipOrder.PaymentBy = (dt_MembershipOrder.Rows[0]["PaymentBy"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["PaymentBy"].ToString() : null);
                objMembers.objMembershipOrder.AdminComment = (dt_MembershipOrder.Rows[0]["AdminComment"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["AdminComment"].ToString() : null);
                objMembers.objMembershipOrder.PaymentStatus = (dt_MembershipOrder.Rows[0]["PaymentStatus"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["PaymentStatus"].ToString() : null);
                objMembers.objMembershipOrder.PaymentMethod = (dt_MembershipOrder.Rows[0]["PaymentMethod"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["PaymentMethod"].ToString() : null);
                objMembers.objMembershipOrder.MembershipType = (dt_MembershipOrder.Rows[0]["MembershipType"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["MembershipType"].ToString() : null);
                objMembers.objMembershipOrder.UserComment = (dt_MembershipOrder.Rows[0]["UserComment"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["UserComment"].ToString() : null);
                objMembers.objMembershipOrder.OrderDate = (dt_MembershipOrder.Rows[0]["OrderDate"] != DBNull.Value ? Convert.ToDateTime(dt_MembershipOrder.Rows[0]["OrderDate"]) : DateTime.MinValue);
                objMembers.objMembershipOrder.ExpiryDate = (dt_MembershipOrder.Rows[0]["ExpiryDate"] != DBNull.Value ? Convert.ToDateTime(dt_MembershipOrder.Rows[0]["ExpiryDate"]) : DateTime.MinValue);
                objMembers.objMembershipOrder.ChequeDate = (dt_MembershipOrder.Rows[0]["ChequeDate"] != DBNull.Value ? Convert.ToDateTime(dt_MembershipOrder.Rows[0]["ChequeDate"]) : DateTime.MinValue);
                objMembers.objMembershipOrder.ChequeNo = dt_MembershipOrder.Rows[0]["ChequeNo"].ToString();
                objMembers.objMembershipOrder.BankName = dt_MembershipOrder.Rows[0]["BankName"].ToString();
                //objMembers.objMembershipOrder.UpdatedTime = Convert.ToDateTime(dt_MembershipOrder.Rows[0]["UpdatedTime"]);
               objMembers.objMembershipOrder.UpdatedBy = dt_MembershipOrder.Rows[0]["UpdatedBy"].ToString();

            }

            objMembers.lstMembershipOrder = lstMembershipOrder;



            if (dt_MembersInfo.Rows.Count == 1)
            {
                objMembers.MemberInfoId = Convert.ToInt64(dt_MembersInfo.Rows[0]["MemberInfoId"]);
                objMembers.MemberId = Convert.ToInt64(dt_MembersInfo.Rows[0]["MemberId"]);
                objMembers.MBachelors = (dt_MembersInfo.Rows[0]["MBachelors"] != DBNull.Value ? dt_MembersInfo.Rows[0]["MBachelors"].ToString() : null);
                objMembers.MAdvanced = (dt_MembersInfo.Rows[0]["MAdvanced"] != DBNull.Value ? dt_MembersInfo.Rows[0]["MAdvanced"].ToString() : null);
                objMembers.SPouseBachelors = (dt_MembersInfo.Rows[0]["SPouseBachelors"] != DBNull.Value ? dt_MembersInfo.Rows[0]["SPouseBachelors"].ToString() : null);
                objMembers.SPouseAdvanced = (dt_MembersInfo.Rows[0]["SPouseAdvanced"] != DBNull.Value ? dt_MembersInfo.Rows[0]["SPouseAdvanced"].ToString() : null);
                objMembers.SelfCity = (dt_MembersInfo.Rows[0]["SelfCity"] != DBNull.Value ? dt_MembersInfo.Rows[0]["SelfCity"].ToString() : null);
                objMembers.SelfDistrict = (dt_MembersInfo.Rows[0]["SelfDistrict"] != DBNull.Value ? dt_MembersInfo.Rows[0]["SelfDistrict"].ToString() : null);
                objMembers.SelfPhoneNo = (dt_MembersInfo.Rows[0]["SelfPhoneNo"] != DBNull.Value ? dt_MembersInfo.Rows[0]["SelfPhoneNo"].ToString() : null);
                objMembers.SelfName = (dt_MembersInfo.Rows[0]["SelfName"] != DBNull.Value ? dt_MembersInfo.Rows[0]["SelfName"].ToString() : null); 
                objMembers.SelfRelation = (dt_MembersInfo.Rows[0]["SelfRelation"] != DBNull.Value ? dt_MembersInfo.Rows[0]["SelfRelation"].ToString() : null); 
                objMembers.SpouseCity = (dt_MembersInfo.Rows[0]["SpouseCity"] != DBNull.Value ? dt_MembersInfo.Rows[0]["SpouseCity"].ToString() : null);
                objMembers.SpouseDistrict = (dt_MembersInfo.Rows[0]["SpouseDistrict"] != DBNull.Value ? dt_MembersInfo.Rows[0]["SpouseDistrict"].ToString() : null);
                objMembers.SpousePhoneNo = (dt_MembersInfo.Rows[0]["SpousePhoneNo"] != DBNull.Value ? dt_MembersInfo.Rows[0]["SpousePhoneNo"].ToString() : null);
                objMembers.SpouseName = (dt_MembersInfo.Rows[0]["SpouseName"] != DBNull.Value ? dt_MembersInfo.Rows[0]["SpouseName"].ToString() : null); 
                objMembers.SpouseRelation = (dt_MembersInfo.Rows[0]["SpouseRelation"] != DBNull.Value ? dt_MembersInfo.Rows[0]["SpouseRelation"].ToString() : null); 
                objMembers.Donate = (dt_MembersInfo.Rows[0]["Donate"] != DBNull.Value ? dt_MembersInfo.Rows[0]["Donate"].ToString() : null);
                objMembers.FrequencyofDonation = (dt_MembersInfo.Rows[0]["FrequencyofDonation"] != DBNull.Value ? dt_MembersInfo.Rows[0]["FrequencyofDonation"].ToString() : null);
                objMembers.DonationAmount = (dt_MembersInfo.Rows[0]["Amount"] != DBNull.Value ? Convert.ToDecimal(dt_MembersInfo.Rows[0]["Amount"]) : 0);
                objMembers.TypeofCard = (dt_MembersInfo.Rows[0]["TypeofCard"] != DBNull.Value ? dt_MembersInfo.Rows[0]["TypeofCard"].ToString() : null);
                objMembers.CardNumber = (dt_MembersInfo.Rows[0]["CardNumber"] != DBNull.Value ? dt_MembersInfo.Rows[0]["CardNumber"].ToString() : null);
                objMembers.ExipryMonth = (dt_MembersInfo.Rows[0]["ExipryMonth"] != DBNull.Value ? dt_MembersInfo.Rows[0]["ExipryMonth"].ToString() : null);
                objMembers.ExipryYear = (dt_MembersInfo.Rows[0]["ExipryYear"] != DBNull.Value ? dt_MembersInfo.Rows[0]["ExipryYear"].ToString() : null);
                objMembers.Cvv = (dt_MembersInfo.Rows[0]["Cvv"] != DBNull.Value ? dt_MembersInfo.Rows[0]["Cvv"].ToString() : null);
                objMembers.SameAdress = (dt_MembersInfo.Rows[0]["SameAdress"] != DBNull.Value ? dt_MembersInfo.Rows[0]["SameAdress"].ToString() : null);
                objMembers.BillingAdress = (dt_MembersInfo.Rows[0]["BillingAdress"] != DBNull.Value ? dt_MembersInfo.Rows[0]["BillingAdress"].ToString() : null);
                objMembers.BillingCity = (dt_MembersInfo.Rows[0]["BillingCity"] != DBNull.Value ? dt_MembersInfo.Rows[0]["BillingCity"].ToString() : null);
                objMembers.BillingState = (dt_MembersInfo.Rows[0]["BillingState"] != DBNull.Value ? dt_MembersInfo.Rows[0]["BillingState"].ToString() : null);
                objMembers.BillingZipCode = (dt_MembersInfo.Rows[0]["BillingZipCode"] != DBNull.Value ? dt_MembersInfo.Rows[0]["BillingZipCode"].ToString() : null);
                objMembers.NATSInsurance = (dt_MembersInfo.Rows[0]["NATSInsurance"] != DBNull.Value ? dt_MembersInfo.Rows[0]["NATSInsurance"].ToString() : null);
                objMembers.BeneficiaryName = (dt_MembersInfo.Rows[0]["BeneficiaryName"] != DBNull.Value ? dt_MembersInfo.Rows[0]["BeneficiaryName"].ToString() : null);
                objMembers.UniversityName = (dt_MembersInfo.Rows[0]["UniversityName"] != DBNull.Value ? dt_MembersInfo.Rows[0]["UniversityName"].ToString() : null);
                objMembers.VolunteerOrganization = (dt_MembersInfo.Rows[0]["VolunteerOrganization"] != DBNull.Value ? dt_MembersInfo.Rows[0]["VolunteerOrganization"].ToString() : null);
                objMembers.AuthorNATS = (dt_MembersInfo.Rows[0]["AuthorNATS"] != DBNull.Value ? dt_MembersInfo.Rows[0]["AuthorNATS"].ToString() : null);
                objMembers.AdressNATS = (dt_MembersInfo.Rows[0]["AdressNATS"] != DBNull.Value ? dt_MembersInfo.Rows[0]["AdressNATS"].ToString() : null);
                objMembers.PhoneNoNATS = (dt_MembersInfo.Rows[0]["PhoneNoNATS"] != DBNull.Value ? dt_MembersInfo.Rows[0]["PhoneNoNATS"].ToString() : null);
                objMembers.EmailNATS = (dt_MembersInfo.Rows[0]["EmailNATS"] != DBNull.Value ? dt_MembersInfo.Rows[0]["EmailNATS"].ToString() : null);
                objMembers.KnowledgeNATS = (dt_MembersInfo.Rows[0]["KnowledgeNATS"] != DBNull.Value ? dt_MembersInfo.Rows[0]["KnowledgeNATS"].ToString() : null);
                objMembers.Field1 = (dt_MembersInfo.Rows[0]["Field1"] != DBNull.Value ? dt_MembersInfo.Rows[0]["Field1"].ToString() : null);
                objMembers.Field2 = (dt_MembersInfo.Rows[0]["Field2"] != DBNull.Value ? dt_MembersInfo.Rows[0]["Field2"].ToString() : null);
                objMembers.Field3 = (dt_MembersInfo.Rows[0]["Field3"] != DBNull.Value ? dt_MembersInfo.Rows[0]["Field3"].ToString() : null);
                objMembers.NoofPayments = (dt_MembersInfo.Rows[0]["NoofPayments"] != DBNull.Value ? Convert.ToInt64(dt_MembersInfo.Rows[0]["NoofPayments"]) : 0);

                


            }
            // ✅ Add Table[4] for MemberSubscriptions
            if (ds0.Tables.Count > 4)
            {
                DataTable dt_MemberSubscriptions = ds0.Tables[4];

                if (dt_MemberSubscriptions.Rows.Count > 0)
                {
                    DataRow dr = dt_MemberSubscriptions.Rows[0];

                    objMembers.SubscriptionId = dr["SubscriotionId"] != DBNull.Value ? dr["SubscriotionId"].ToString() : null;
                   
                }
            }
            objMembers.lstMembershipOrder = lstMembershipOrder;

            return objMembers;
        }

        public Entities.Members GetMemberFullDetailsBySpouseCell(string SpouseCell, string LastName, ref int status)
        {
            DataSet ds0 = _Members1.GetMemberFullDetailsBySpouseCell(SpouseCell, LastName, ref status);

            DataTable dt_Members = ds0.Tables[0];
            DataTable dt_ChildrenInfo = ds0.Tables[1];
            DataTable dt_MembershipOrder = ds0.Tables[2];


            Entities.Members objMembers = new Entities.Members();
            List<Entities.ChildrenInfo> lstChildrenInfo = new List<Entities.ChildrenInfo>();
            List<Entities.MembershipOrders> lstMembershipOrder = new List<Entities.MembershipOrders>();

            if (dt_Members.Rows.Count == 1)
            {

                objMembers.MemberId = Convert.ToInt64(dt_Members.Rows[0]["MemberId"]);
                objMembers.Title = dt_Members.Rows[0]["Title"].ToString();
                objMembers.Email = dt_Members.Rows[0]["Email"].ToString();
                objMembers.FirstName = dt_Members.Rows[0]["FirstName"].ToString();
                objMembers.LastName = dt_Members.Rows[0]["LastName"].ToString();
                objMembers.ProfileImage = (dt_Members.Rows[0]["ProfileImage"] != DBNull.Value ? dt_Members.Rows[0]["ProfileImage"].ToString() : null);
                objMembers.Occupation = (dt_Members.Rows[0]["Occupation"] != DBNull.Value ? dt_Members.Rows[0]["Occupation"].ToString() : null);
                objMembers.MemberAge = (dt_Members.Rows[0]["MemberAge"] != DBNull.Value ? dt_Members.Rows[0]["MemberAge"].ToString() : null);
                objMembers.MemberSkils = (dt_Members.Rows[0]["Occupation"] != DBNull.Value ? dt_Members.Rows[0]["MemberSkils"].ToString() : null);
                objMembers.SpouseSkils = (dt_Members.Rows[0]["SpouseSkils"] != DBNull.Value ? dt_Members.Rows[0]["SpouseSkils"].ToString() : null);
                objMembers.SpouseFirstName = (dt_Members.Rows[0]["SpouseFirstName"] != DBNull.Value ? dt_Members.Rows[0]["SpouseFirstName"].ToString() : null);
                objMembers.SpouseLastName = (dt_Members.Rows[0]["SpouseLastName"] != DBNull.Value ? dt_Members.Rows[0]["SpouseLastName"].ToString() : null);
                objMembers.SpouseOccupation = (dt_Members.Rows[0]["SpouseOccupation"] != DBNull.Value ? dt_Members.Rows[0]["SpouseOccupation"].ToString() : null);
                objMembers.SpouseEmail = (dt_Members.Rows[0]["SpouseEmail"] != DBNull.Value ? dt_Members.Rows[0]["SpouseEmail"].ToString() : null);
                objMembers.SpouseCell = (dt_Members.Rows[0]["SpouseCell"] != DBNull.Value ? dt_Members.Rows[0]["SpouseCell"].ToString() : null);
                objMembers.Address = (dt_Members.Rows[0]["Address"] != DBNull.Value ? dt_Members.Rows[0]["Address"].ToString() : null);
                objMembers.City = (dt_Members.Rows[0]["City"] != DBNull.Value ? dt_Members.Rows[0]["City"].ToString() : null);
                objMembers.State = (dt_Members.Rows[0]["State"] != DBNull.Value ? dt_Members.Rows[0]["State"].ToString() : null);
                objMembers.ZipCode = (dt_Members.Rows[0]["ZipCode"] != DBNull.Value ? dt_Members.Rows[0]["ZipCode"].ToString() : null);
                objMembers.HomePhone = (dt_Members.Rows[0]["HomePhone"] != DBNull.Value ? dt_Members.Rows[0]["HomePhone"].ToString() : null);
                objMembers.MobilePhone = (dt_Members.Rows[0]["MobilePhone"] != DBNull.Value ? dt_Members.Rows[0]["MobilePhone"].ToString() : null);
                objMembers.IsApproved = Convert.ToBoolean(dt_Members.Rows[0]["IsApproved"]);
                objMembers.IsLockedOut = (dt_Members.Rows[0]["IsLockedOut"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsLockedOut"]) : false);
                objMembers.MemberAmount = (dt_Members.Rows[0]["MemberAmount"] != DBNull.Value ? Convert.ToDecimal(dt_Members.Rows[0]["MemberAmount"]) : 0);
                objMembers.IsActivated = (dt_Members.Rows[0]["IsLockedOut"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsLockedOut"]) : false);
                objMembers.DateActivated = (dt_Members.Rows[0]["DateActivated"] != DBNull.Value ? Convert.ToDateTime(dt_Members.Rows[0]["DateActivated"]) : DateTime.MinValue);
                objMembers.MembershipTypeId = Convert.ToInt64(dt_Members.Rows[0]["MembershipTypeId"]);
                objMembers.MembershipType = dt_Members.Rows[0]["MembershipType"].ToString();
                objMembers.IsVolunteer = (dt_Members.Rows[0]["IsVolunteer"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsVolunteer"]) : false);
                objMembers.IsTeluguorigin = (dt_Members.Rows[0]["IsTeluguorigin"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsTeluguorigin"]) : false);
                objMembers.Comments = (dt_Members.Rows[0]["Comments"] != DBNull.Value ? dt_Members.Rows[0]["Comments"].ToString() : null);
                objMembers.ReferredBy = (dt_Members.Rows[0]["ReferredBy"] != DBNull.Value ? dt_Members.Rows[0]["ReferredBy"].ToString() : null);
                objMembers.MobilePhone = (dt_Members.Rows[0]["MobilePhone"] != DBNull.Value ? dt_Members.Rows[0]["MobilePhone"].ToString() : null);
                objMembers.InsertedTime = Convert.ToDateTime(dt_Members.Rows[0]["InsertedTime"]);
                objMembers.UpdatedTime = Convert.ToDateTime(dt_Members.Rows[0]["UpdatedTime"]);
                objMembers.Fax = (dt_Members.Rows[0]["Fax"] != DBNull.Value ? dt_Members.Rows[0]["Fax"].ToString() : null);
                objMembers.WebsiteAddress = (dt_Members.Rows[0]["WebsiteAddress"] != DBNull.Value ? dt_Members.Rows[0]["WebsiteAddress"].ToString() : null);
                objMembers.Address2 = (dt_Members.Rows[0]["Address2"] != DBNull.Value ? dt_Members.Rows[0]["Address2"].ToString() : null);
            }

            if (dt_ChildrenInfo.Rows.Count != 0)
            {

                foreach (DataRow dr in dt_ChildrenInfo.Rows)
                {
                    Entities.ChildrenInfo objChildrenInfo = new Entities.ChildrenInfo();

                    objChildrenInfo.ChildrenInfoId = Convert.ToInt64(dr["ChildrenInfoId"]);
                    objChildrenInfo.MemberId = Convert.ToInt64(dr["MemberId"]);
                    objChildrenInfo.FirstName = dr["FirstName"].ToString();
                    objChildrenInfo.LastName = dr["LastName"].ToString();
                    objChildrenInfo.Age = (dr["Age"] != DBNull.Value ? Convert.ToInt32(dr["Age"].ToString()) : 0);
                    objChildrenInfo.Relationship = dr["Relationship"].ToString();

                    lstChildrenInfo.Add(objChildrenInfo);
                }
            }

            objMembers.lstChildrenInfo = lstChildrenInfo;

            if (dt_MembershipOrder.Rows.Count == 1)
            {
                objMembers.objMembershipOrder.MembershipOrderId = Convert.ToInt64(dt_MembershipOrder.Rows[0]["MembershipOrderId"]);


            }

            objMembers.lstMembershipOrder = lstMembershipOrder;

            return objMembers;
        }

        public Entities.Members GetMemberFullDetailsBySpouse(string SpouseCell, ref int status)
        {
            DataSet ds0 = _Members1.GetMemberFullDetailsBySpouse(SpouseCell, ref status);

            DataTable dt_Members = ds0.Tables[0];
            DataTable dt_ChildrenInfo = ds0.Tables[1];
            DataTable dt_MembershipOrder = ds0.Tables[2];


            Entities.Members objMembers = new Entities.Members();
            List<Entities.ChildrenInfo> lstChildrenInfo = new List<Entities.ChildrenInfo>();
            List<Entities.MembershipOrders> lstMembershipOrder = new List<Entities.MembershipOrders>();

            if (dt_Members.Rows.Count == 1)
            {

                objMembers.MemberId = Convert.ToInt64(dt_Members.Rows[0]["MemberId"]);
                objMembers.Title = dt_Members.Rows[0]["Title"].ToString();
                objMembers.Email = dt_Members.Rows[0]["Email"].ToString();
                objMembers.FirstName = dt_Members.Rows[0]["FirstName"].ToString();
                objMembers.LastName = dt_Members.Rows[0]["LastName"].ToString();
                objMembers.ProfileImage = (dt_Members.Rows[0]["ProfileImage"] != DBNull.Value ? dt_Members.Rows[0]["ProfileImage"].ToString() : null);
                objMembers.Occupation = (dt_Members.Rows[0]["Occupation"] != DBNull.Value ? dt_Members.Rows[0]["Occupation"].ToString() : null);
                objMembers.MemberAge = (dt_Members.Rows[0]["MemberAge"] != DBNull.Value ? dt_Members.Rows[0]["MemberAge"].ToString() : null);
                objMembers.MemberSkils = (dt_Members.Rows[0]["Occupation"] != DBNull.Value ? dt_Members.Rows[0]["MemberSkils"].ToString() : null);
                objMembers.SpouseSkils = (dt_Members.Rows[0]["SpouseSkils"] != DBNull.Value ? dt_Members.Rows[0]["SpouseSkils"].ToString() : null);
                objMembers.SpouseFirstName = (dt_Members.Rows[0]["SpouseFirstName"] != DBNull.Value ? dt_Members.Rows[0]["SpouseFirstName"].ToString() : null);
                objMembers.SpouseLastName = (dt_Members.Rows[0]["SpouseLastName"] != DBNull.Value ? dt_Members.Rows[0]["SpouseLastName"].ToString() : null);
                objMembers.SpouseOccupation = (dt_Members.Rows[0]["SpouseOccupation"] != DBNull.Value ? dt_Members.Rows[0]["SpouseOccupation"].ToString() : null);
                objMembers.SpouseEmail = (dt_Members.Rows[0]["SpouseEmail"] != DBNull.Value ? dt_Members.Rows[0]["SpouseEmail"].ToString() : null);
                objMembers.SpouseCell = (dt_Members.Rows[0]["SpouseCell"] != DBNull.Value ? dt_Members.Rows[0]["SpouseCell"].ToString() : null);
                objMembers.Address = (dt_Members.Rows[0]["Address"] != DBNull.Value ? dt_Members.Rows[0]["Address"].ToString() : null);
                objMembers.City = (dt_Members.Rows[0]["City"] != DBNull.Value ? dt_Members.Rows[0]["City"].ToString() : null);
                objMembers.State = (dt_Members.Rows[0]["State"] != DBNull.Value ? dt_Members.Rows[0]["State"].ToString() : null);
                objMembers.ZipCode = (dt_Members.Rows[0]["ZipCode"] != DBNull.Value ? dt_Members.Rows[0]["ZipCode"].ToString() : null);
                objMembers.HomePhone = (dt_Members.Rows[0]["HomePhone"] != DBNull.Value ? dt_Members.Rows[0]["HomePhone"].ToString() : null);
                objMembers.MobilePhone = (dt_Members.Rows[0]["MobilePhone"] != DBNull.Value ? dt_Members.Rows[0]["MobilePhone"].ToString() : null);
                objMembers.IsApproved = Convert.ToBoolean(dt_Members.Rows[0]["IsApproved"]);
                objMembers.IsLockedOut = (dt_Members.Rows[0]["IsLockedOut"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsLockedOut"]) : false);
                objMembers.MemberAmount = (dt_Members.Rows[0]["MemberAmount"] != DBNull.Value ? Convert.ToDecimal(dt_Members.Rows[0]["MemberAmount"]) : 0);
                objMembers.IsActivated = (dt_Members.Rows[0]["IsLockedOut"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsLockedOut"]) : false);
                objMembers.DateActivated = (dt_Members.Rows[0]["DateActivated"] != DBNull.Value ? Convert.ToDateTime(dt_Members.Rows[0]["DateActivated"]) : DateTime.MinValue);
                objMembers.MembershipTypeId = Convert.ToInt64(dt_Members.Rows[0]["MembershipTypeId"]);
                objMembers.MembershipType = dt_Members.Rows[0]["MembershipType"].ToString();
                objMembers.IsVolunteer = (dt_Members.Rows[0]["IsVolunteer"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsVolunteer"]) : false);
                objMembers.IsTeluguorigin = (dt_Members.Rows[0]["IsTeluguorigin"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsTeluguorigin"]) : false);
                objMembers.Comments = (dt_Members.Rows[0]["Comments"] != DBNull.Value ? dt_Members.Rows[0]["Comments"].ToString() : null);
                objMembers.ReferredBy = (dt_Members.Rows[0]["ReferredBy"] != DBNull.Value ? dt_Members.Rows[0]["ReferredBy"].ToString() : null);
                objMembers.MobilePhone = (dt_Members.Rows[0]["MobilePhone"] != DBNull.Value ? dt_Members.Rows[0]["MobilePhone"].ToString() : null);
                objMembers.InsertedTime = Convert.ToDateTime(dt_Members.Rows[0]["InsertedTime"]);
                objMembers.UpdatedTime = Convert.ToDateTime(dt_Members.Rows[0]["UpdatedTime"]);
                objMembers.Fax = (dt_Members.Rows[0]["Fax"] != DBNull.Value ? dt_Members.Rows[0]["Fax"].ToString() : null);
                objMembers.WebsiteAddress = (dt_Members.Rows[0]["WebsiteAddress"] != DBNull.Value ? dt_Members.Rows[0]["WebsiteAddress"].ToString() : null);
                objMembers.Address2 = (dt_Members.Rows[0]["Address2"] != DBNull.Value ? dt_Members.Rows[0]["Address2"].ToString() : null);
            }

            if (dt_ChildrenInfo.Rows.Count != 0)
            {

                foreach (DataRow dr in dt_ChildrenInfo.Rows)
                {
                    Entities.ChildrenInfo objChildrenInfo = new Entities.ChildrenInfo();

                    objChildrenInfo.ChildrenInfoId = Convert.ToInt64(dr["ChildrenInfoId"]);
                    objChildrenInfo.MemberId = Convert.ToInt64(dr["MemberId"]);
                    objChildrenInfo.FirstName = dr["FirstName"].ToString();
                    objChildrenInfo.LastName = dr["LastName"].ToString();
                    objChildrenInfo.Age = (dr["Age"] != DBNull.Value ? Convert.ToInt32(dr["Age"].ToString()) : 0);
                    objChildrenInfo.Relationship = dr["Relationship"].ToString();

                    lstChildrenInfo.Add(objChildrenInfo);
                }
            }

            objMembers.lstChildrenInfo = lstChildrenInfo;

            if (dt_MembershipOrder.Rows.Count == 1)
            {
                objMembers.objMembershipOrder.MembershipOrderId = Convert.ToInt64(dt_MembershipOrder.Rows[0]["MembershipOrderId"]);


            }

            objMembers.lstMembershipOrder = lstMembershipOrder;

            return objMembers;
        }

        public Entities.Members GetMemberFullDetailsByLastName(string LastName, ref int status)
        {
            DataSet ds0 = _Members1.GetMemberFullDetailsByLastName(LastName, ref status);

            DataTable dt_Members = ds0.Tables[0];
            DataTable dt_ChildrenInfo = ds0.Tables[1];
            DataTable dt_MembershipOrder = ds0.Tables[2];


            Entities.Members objMembers = new Entities.Members();
            List<Entities.ChildrenInfo> lstChildrenInfo = new List<Entities.ChildrenInfo>();
            List<Entities.MembershipOrders> lstMembershipOrder = new List<Entities.MembershipOrders>();

            if (dt_Members.Rows.Count == 1)
            {

                objMembers.MemberId = Convert.ToInt64(dt_Members.Rows[0]["MemberId"]);
                objMembers.Title = dt_Members.Rows[0]["Title"].ToString();
                objMembers.Email = dt_Members.Rows[0]["Email"].ToString();
                objMembers.FirstName = dt_Members.Rows[0]["FirstName"].ToString();
                objMembers.LastName = dt_Members.Rows[0]["LastName"].ToString();
                objMembers.ProfileImage = (dt_Members.Rows[0]["ProfileImage"] != DBNull.Value ? dt_Members.Rows[0]["ProfileImage"].ToString() : null);
                objMembers.Occupation = (dt_Members.Rows[0]["Occupation"] != DBNull.Value ? dt_Members.Rows[0]["Occupation"].ToString() : null);
                objMembers.MemberAge = (dt_Members.Rows[0]["MemberAge"] != DBNull.Value ? dt_Members.Rows[0]["MemberAge"].ToString() : null);
                objMembers.MemberSkils = (dt_Members.Rows[0]["Occupation"] != DBNull.Value ? dt_Members.Rows[0]["MemberSkils"].ToString() : null);
                objMembers.SpouseSkils = (dt_Members.Rows[0]["SpouseSkils"] != DBNull.Value ? dt_Members.Rows[0]["SpouseSkils"].ToString() : null);
                objMembers.SpouseFirstName = (dt_Members.Rows[0]["SpouseFirstName"] != DBNull.Value ? dt_Members.Rows[0]["SpouseFirstName"].ToString() : null);
                objMembers.SpouseLastName = (dt_Members.Rows[0]["SpouseLastName"] != DBNull.Value ? dt_Members.Rows[0]["SpouseLastName"].ToString() : null);
                objMembers.SpouseOccupation = (dt_Members.Rows[0]["SpouseOccupation"] != DBNull.Value ? dt_Members.Rows[0]["SpouseOccupation"].ToString() : null);
                objMembers.SpouseEmail = (dt_Members.Rows[0]["SpouseEmail"] != DBNull.Value ? dt_Members.Rows[0]["SpouseEmail"].ToString() : null);
                objMembers.SpouseCell = (dt_Members.Rows[0]["SpouseCell"] != DBNull.Value ? dt_Members.Rows[0]["SpouseCell"].ToString() : null);
                objMembers.Address = (dt_Members.Rows[0]["Address"] != DBNull.Value ? dt_Members.Rows[0]["Address"].ToString() : null);
                objMembers.City = (dt_Members.Rows[0]["City"] != DBNull.Value ? dt_Members.Rows[0]["City"].ToString() : null);
                objMembers.State = (dt_Members.Rows[0]["State"] != DBNull.Value ? dt_Members.Rows[0]["State"].ToString() : null);
                objMembers.ZipCode = (dt_Members.Rows[0]["ZipCode"] != DBNull.Value ? dt_Members.Rows[0]["ZipCode"].ToString() : null);
                objMembers.HomePhone = (dt_Members.Rows[0]["HomePhone"] != DBNull.Value ? dt_Members.Rows[0]["HomePhone"].ToString() : null);
                objMembers.MobilePhone = (dt_Members.Rows[0]["MobilePhone"] != DBNull.Value ? dt_Members.Rows[0]["MobilePhone"].ToString() : null);
                objMembers.IsApproved = Convert.ToBoolean(dt_Members.Rows[0]["IsApproved"]);
                objMembers.IsLockedOut = (dt_Members.Rows[0]["IsLockedOut"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsLockedOut"]) : false);
                objMembers.MemberAmount = (dt_Members.Rows[0]["MemberAmount"] != DBNull.Value ? Convert.ToDecimal(dt_Members.Rows[0]["MemberAmount"]) : 0);
                objMembers.IsActivated = (dt_Members.Rows[0]["IsLockedOut"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsLockedOut"]) : false);
                objMembers.DateActivated = (dt_Members.Rows[0]["DateActivated"] != DBNull.Value ? Convert.ToDateTime(dt_Members.Rows[0]["DateActivated"]) : DateTime.MinValue);
                objMembers.MembershipTypeId = Convert.ToInt64(dt_Members.Rows[0]["MembershipTypeId"]);
                objMembers.MembershipType = dt_Members.Rows[0]["MembershipType"].ToString();
                objMembers.IsVolunteer = (dt_Members.Rows[0]["IsVolunteer"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsVolunteer"]) : false);
                objMembers.IsTeluguorigin = (dt_Members.Rows[0]["IsTeluguorigin"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsTeluguorigin"]) : false);
                objMembers.Comments = (dt_Members.Rows[0]["Comments"] != DBNull.Value ? dt_Members.Rows[0]["Comments"].ToString() : null);
                objMembers.ReferredBy = (dt_Members.Rows[0]["ReferredBy"] != DBNull.Value ? dt_Members.Rows[0]["ReferredBy"].ToString() : null);
                objMembers.MobilePhone = (dt_Members.Rows[0]["MobilePhone"] != DBNull.Value ? dt_Members.Rows[0]["MobilePhone"].ToString() : null);
                objMembers.InsertedTime = Convert.ToDateTime(dt_Members.Rows[0]["InsertedTime"]);
                objMembers.UpdatedTime = Convert.ToDateTime(dt_Members.Rows[0]["UpdatedTime"]);
                objMembers.Fax = (dt_Members.Rows[0]["Fax"] != DBNull.Value ? dt_Members.Rows[0]["Fax"].ToString() : null);
                objMembers.WebsiteAddress = (dt_Members.Rows[0]["WebsiteAddress"] != DBNull.Value ? dt_Members.Rows[0]["WebsiteAddress"].ToString() : null);
                objMembers.Address2 = (dt_Members.Rows[0]["Address2"] != DBNull.Value ? dt_Members.Rows[0]["Address2"].ToString() : null);
            }

            if (dt_ChildrenInfo.Rows.Count != 0)
            {

                foreach (DataRow dr in dt_ChildrenInfo.Rows)
                {
                    Entities.ChildrenInfo objChildrenInfo = new Entities.ChildrenInfo();

                    objChildrenInfo.ChildrenInfoId = Convert.ToInt64(dr["ChildrenInfoId"]);
                    objChildrenInfo.MemberId = Convert.ToInt64(dr["MemberId"]);
                    objChildrenInfo.FirstName = dr["FirstName"].ToString();
                    objChildrenInfo.LastName = dr["LastName"].ToString();
                    objChildrenInfo.Age = (dr["Age"] != DBNull.Value ? Convert.ToInt32(dr["Age"].ToString()) : 0);
                    objChildrenInfo.Relationship = dr["Relationship"].ToString();

                    lstChildrenInfo.Add(objChildrenInfo);
                }
            }

            objMembers.lstChildrenInfo = lstChildrenInfo;

            if (dt_MembershipOrder.Rows.Count == 1)
            {
                objMembers.objMembershipOrder.MembershipOrderId = Convert.ToInt64(dt_MembershipOrder.Rows[0]["MembershipOrderId"]);


            }

            objMembers.lstMembershipOrder = lstMembershipOrder;

            return objMembers;
        }

        public Entities.MembershipOrders GetMemberOrderById(Int64 MemberOrderId, ref int status)
        {
            DataTable dt_MembershipOrder = _Members1.GetMemberOrderById(MemberOrderId, ref status);

            Entities.MembershipOrders objMembershipOrder = new Entities.MembershipOrders();
           
            if (dt_MembershipOrder.Rows.Count == 1)
            {
                objMembershipOrder.MembershipOrderId = Convert.ToInt64(dt_MembershipOrder.Rows[0]["MembershipOrderId"]);
                objMembershipOrder.MemberId = (dt_MembershipOrder.Rows[0]["MemberId"] != DBNull.Value ? Convert.ToInt64(dt_MembershipOrder.Rows[0]["MemberId"]) : 0);
                objMembershipOrder.MembershipTypeId = (dt_MembershipOrder.Rows[0]["MembershipTypeId"] != DBNull.Value ? Convert.ToInt64(dt_MembershipOrder.Rows[0]["MembershipTypeId"]) : 0);
                objMembershipOrder.Amount = (dt_MembershipOrder.Rows[0]["Amount"] != DBNull.Value ? Convert.ToDecimal(dt_MembershipOrder.Rows[0]["Amount"]) : 0);
                objMembershipOrder.TransactionId = (dt_MembershipOrder.Rows[0]["TransactionId"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["TransactionId"].ToString() : null);
                objMembershipOrder.PaymentStatusId = (dt_MembershipOrder.Rows[0]["PaymentStatusId"] != DBNull.Value ? Convert.ToInt64(dt_MembershipOrder.Rows[0]["PaymentStatusId"]) : 0);
                objMembershipOrder.PaymentMethodId = (dt_MembershipOrder.Rows[0]["PaymentMethodId"] != DBNull.Value ? Convert.ToInt64(dt_MembershipOrder.Rows[0]["PaymentMethodId"]) : 0);
                objMembershipOrder.PaymentBy = (dt_MembershipOrder.Rows[0]["PaymentBy"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["PaymentBy"].ToString() : null);
                objMembershipOrder.AdminComment = (dt_MembershipOrder.Rows[0]["AdminComment"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["AdminComment"].ToString() : null);
                objMembershipOrder.PaymentStatus = (dt_MembershipOrder.Rows[0]["PaymentStatus"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["PaymentStatus"].ToString() : null);
                objMembershipOrder.PaymentMethod = (dt_MembershipOrder.Rows[0]["PaymentMethod"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["PaymentMethod"].ToString() : null);
                objMembershipOrder.MembershipType = (dt_MembershipOrder.Rows[0]["MembershipType"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["MembershipType"].ToString() : null);
                objMembershipOrder.UserComment = (dt_MembershipOrder.Rows[0]["UserComment"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["UserComment"].ToString() : null);
                objMembershipOrder.OrderDate = (dt_MembershipOrder.Rows[0]["OrderDate"] != DBNull.Value ? Convert.ToDateTime(dt_MembershipOrder.Rows[0]["OrderDate"]) : DateTime.MinValue);
                objMembershipOrder.ExpiryDate = (dt_MembershipOrder.Rows[0]["ExpiryDate"] != DBNull.Value ? Convert.ToDateTime(dt_MembershipOrder.Rows[0]["ExpiryDate"]) : DateTime.MinValue);
                objMembershipOrder.BankName = (dt_MembershipOrder.Rows[0]["BankName"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["BankName"].ToString() : null);
                objMembershipOrder.ChequeNo = (dt_MembershipOrder.Rows[0]["ChequeNo"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["ChequeNo"].ToString() : null);
                objMembershipOrder.ChequeDate = (dt_MembershipOrder.Rows[0]["ChequeDate"] != DBNull.Value ? Convert.ToDateTime(dt_MembershipOrder.Rows[0]["ChequeDate"]) : DateTime.MinValue);
                objMembershipOrder.UpdatedTime = Convert.ToDateTime(dt_MembershipOrder.Rows[0]["UpdatedTime"]);
                objMembershipOrder.UpdatedBy = dt_MembershipOrder.Rows[0]["UpdatedBy"].ToString();

            }
            return objMembershipOrder;
        }

        //public Entities.Members GetMemberFullDetailsByTitle(string Title, ref int status)
        //{
        //    DataSet ds0 = _Members1.GetMemberFullDetailsByTitle(Title, ref status);

        //    DataTable dt_Members = ds0.Tables[0];
        //    DataTable dt_ChildrenInfo = ds0.Tables[1];
        //    DataTable dt_MembershipOrder = ds0.Tables[2];


        //    Entities.Members objMembers = new Entities.Members();
        //    List<Entities.ChildrenInfo> lstChildrenInfo = new List<Entities.ChildrenInfo>();
        //    List<Entities.MembershipOrders> lstMembershipOrder = new List<Entities.MembershipOrders>();

        //    if (dt_Members.Rows.Count == 1)
        //    {

        //        objMembers.MemberId = Convert.ToInt64(dt_Members.Rows[0]["MemberId"]);
        //        objMembers.Title = dt_Members.Rows[0]["Title"].ToString();
        //        objMembers.Email = dt_Members.Rows[0]["Email"].ToString();
        //        objMembers.FirstName = dt_Members.Rows[0]["FirstName"].ToString();
        //        objMembers.LastName = dt_Members.Rows[0]["LastName"].ToString();
        //        objMembers.ProfileImage = (dt_Members.Rows[0]["ProfileImage"] != DBNull.Value ? dt_Members.Rows[0]["ProfileImage"].ToString() : null);
        //        objMembers.Occupation = (dt_Members.Rows[0]["Occupation"] != DBNull.Value ? dt_Members.Rows[0]["Occupation"].ToString() : null);
        //        objMembers.SpouseFirstName = (dt_Members.Rows[0]["SpouseFirstName"] != DBNull.Value ? dt_Members.Rows[0]["SpouseFirstName"].ToString() : null);
        //        objMembers.SpouseLastName = (dt_Members.Rows[0]["SpouseLastName"] != DBNull.Value ? dt_Members.Rows[0]["SpouseLastName"].ToString() : null);
        //        objMembers.SpouseOccupation = (dt_Members.Rows[0]["SpouseOccupation"] != DBNull.Value ? dt_Members.Rows[0]["SpouseOccupation"].ToString() : null);
        //        objMembers.SpouseEmail = (dt_Members.Rows[0]["SpouseEmail"] != DBNull.Value ? dt_Members.Rows[0]["SpouseEmail"].ToString() : null);
        //        objMembers.SpouseCell = (dt_Members.Rows[0]["SpouseCell"] != DBNull.Value ? dt_Members.Rows[0]["SpouseCell"].ToString() : null);
        //        objMembers.Address = (dt_Members.Rows[0]["Address"] != DBNull.Value ? dt_Members.Rows[0]["Address"].ToString() : null);
        //        objMembers.City = (dt_Members.Rows[0]["City"] != DBNull.Value ? dt_Members.Rows[0]["City"].ToString() : null);
        //        objMembers.State = (dt_Members.Rows[0]["State"] != DBNull.Value ? dt_Members.Rows[0]["State"].ToString() : null);
        //        objMembers.ZipCode = (dt_Members.Rows[0]["ZipCode"] != DBNull.Value ? dt_Members.Rows[0]["ZipCode"].ToString() : null);
        //        objMembers.HomePhone = (dt_Members.Rows[0]["HomePhone"] != DBNull.Value ? dt_Members.Rows[0]["HomePhone"].ToString() : null);
        //        objMembers.MobilePhone = (dt_Members.Rows[0]["MobilePhone"] != DBNull.Value ? dt_Members.Rows[0]["MobilePhone"].ToString() : null);
        //        objMembers.IsApproved = Convert.ToBoolean(dt_Members.Rows[0]["IsApproved"]);
        //        objMembers.IsLockedOut = Convert.ToBoolean(dt_Members.Rows[0]["IsLockedOut"]);
        //        objMembers.IsActivated = Convert.ToBoolean(dt_Members.Rows[0]["IsActivated"]);
        //        objMembers.DateActivated = (dt_Members.Rows[0]["DateActivated"] != DBNull.Value ? Convert.ToDateTime(dt_Members.Rows[0]["DateActivated"]) : DateTime.MinValue);
        //        objMembers.MembershipTypeId = Convert.ToInt64(dt_Members.Rows[0]["MembershipTypeId"]);
        //        objMembers.MembershipType = dt_Members.Rows[0]["MembershipType"].ToString();
        //        objMembers.IsVolunteer = (dt_Members.Rows[0]["IsVolunteer"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsVolunteer"].ToString()):false);
        //        objMembers.MemberAmount = (dt_Members.Rows[0]["MemberAmount"] != DBNull.Value ? Convert.ToDecimal(dt_Members.Rows[0]["MemberAmount"]) : 0);
        //        objMembers.IsTeluguorigin = Convert.ToBoolean(dt_Members.Rows[0]["IsTeluguorigin"]);
        //        objMembers.Comments = (dt_Members.Rows[0]["Comments"] != DBNull.Value ? dt_Members.Rows[0]["Comments"].ToString() : null);
        //        objMembers.ReferredBy = (dt_Members.Rows[0]["ReferredBy"] != DBNull.Value ? dt_Members.Rows[0]["ReferredBy"].ToString() : null);
        //        objMembers.MobilePhone = (dt_Members.Rows[0]["MobilePhone"] != DBNull.Value ? dt_Members.Rows[0]["MobilePhone"].ToString() : null);
        //        objMembers.InsertedTime = Convert.ToDateTime(dt_Members.Rows[0]["InsertedTime"]);
        //        objMembers.UpdatedTime = Convert.ToDateTime(dt_Members.Rows[0]["UpdatedTime"]);
        //        objMembers.Fax = (dt_Members.Rows[0]["Fax"] != DBNull.Value ? dt_Members.Rows[0]["Fax"].ToString() : null);
        //        objMembers.WebsiteAddress = (dt_Members.Rows[0]["WebsiteAddress"] != DBNull.Value ? dt_Members.Rows[0]["WebsiteAddress"].ToString() : null);
        //        objMembers.Address2 = (dt_Members.Rows[0]["Address2"] != DBNull.Value ? dt_Members.Rows[0]["Address2"].ToString() : null);
        //    }

        //    if (dt_ChildrenInfo.Rows.Count != 0)
        //    {

        //        foreach (DataRow dr in dt_ChildrenInfo.Rows)
        //        {
        //            Entities.ChildrenInfo objChildrenInfo = new Entities.ChildrenInfo();

        //            objChildrenInfo.ChildrenInfoId = Convert.ToInt64(dr["ChildrenInfoId"]);
        //            objChildrenInfo.MemberId = Convert.ToInt64(dr["MemberId"]);
        //            objChildrenInfo.FirstName = dr["FirstName"].ToString();
        //            objChildrenInfo.LastName = dr["LastName"].ToString();
        //            objChildrenInfo.Age = (dr["Age"] != DBNull.Value ? Convert.ToInt32(dr["Age"].ToString()) : 0);
        //            objChildrenInfo.Relationship = dr["Relationship"].ToString();

        //            lstChildrenInfo.Add(objChildrenInfo);
        //        }
        //    }

        //    objMembers.lstChildrenInfo = lstChildrenInfo;

        //    if (dt_MembershipOrder.Rows.Count == 1)
        //    {
        //        objMembers.objMembershipOrder.MembershipOrderId = Convert.ToInt64(dt_MembershipOrder.Rows[0]["MembershipOrderId"]);
        //        objMembers.objMembershipOrder.MemberId = (dt_MembershipOrder.Rows[0]["MemberId"] != DBNull.Value ? Convert.ToInt64(dt_MembershipOrder.Rows[0]["MemberId"]) : 0);
        //        objMembers.objMembershipOrder.MembershipTypeId = (dt_MembershipOrder.Rows[0]["MembershipTypeId"] != DBNull.Value ? Convert.ToInt64(dt_MembershipOrder.Rows[0]["MembershipTypeId"]) : 0);
        //        objMembers.objMembershipOrder.Amount = (dt_MembershipOrder.Rows[0]["Amount"] != DBNull.Value ? Convert.ToDecimal(dt_MembershipOrder.Rows[0]["Amount"]) : 0);
        //        objMembers.objMembershipOrder.TransactionId = (dt_MembershipOrder.Rows[0]["TransactionId"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["TransactionId"].ToString() : null);
        //        objMembers.objMembershipOrder.PaymentStatusId = (dt_MembershipOrder.Rows[0]["PaymentStatusId"] != DBNull.Value ? Convert.ToInt64(dt_MembershipOrder.Rows[0]["PaymentStatusId"]) : 0);
        //        objMembers.objMembershipOrder.PaymentMethodId = (dt_MembershipOrder.Rows[0]["PaymentMethodId"] != DBNull.Value ? Convert.ToInt64(dt_MembershipOrder.Rows[0]["PaymentMethodId"]) : 0);
        //        objMembers.objMembershipOrder.PaymentBy = (dt_MembershipOrder.Rows[0]["PaymentBy"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["PaymentBy"].ToString() : null);
        //        objMembers.objMembershipOrder.AdminComment = (dt_MembershipOrder.Rows[0]["AdminComment"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["AdminComment"].ToString() : null);
        //        objMembers.objMembershipOrder.PaymentStatus = (dt_MembershipOrder.Rows[0]["PaymentStatus"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["PaymentStatus"].ToString() : null);
        //        objMembers.objMembershipOrder.PaymentMethod = (dt_MembershipOrder.Rows[0]["PaymentMethod"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["PaymentMethod"].ToString() : null);
        //        objMembers.objMembershipOrder.MembershipType = (dt_MembershipOrder.Rows[0]["MembershipType"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["MembershipType"].ToString() : null);
        //        objMembers.objMembershipOrder.UserComment = (dt_MembershipOrder.Rows[0]["UserComment"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["UserComment"].ToString() : null);
        //        objMembers.objMembershipOrder.OrderDate = (dt_MembershipOrder.Rows[0]["OrderDate"] != DBNull.Value ? Convert.ToDateTime(dt_MembershipOrder.Rows[0]["OrderDate"]) : DateTime.MinValue);
        //        objMembers.objMembershipOrder.ExpiryDate = (dt_MembershipOrder.Rows[0]["ExpiryDate"] != DBNull.Value ? Convert.ToDateTime(dt_MembershipOrder.Rows[0]["ExpiryDate"]) : DateTime.MinValue);
        //        objMembers.objMembershipOrder.UpdatedTime = Convert.ToDateTime(dt_MembershipOrder.Rows[0]["UpdatedTime"]);
        //        objMembers.objMembershipOrder.UpdatedBy = dt_MembershipOrder.Rows[0]["UpdatedBy"].ToString();

        //    }

        //    return objMembers;
        //}

        public Entities.Members GetMemberFullDetailsByEmail(string Email, ref int status)
        {
            DataSet ds0 = _Members1.GetMemberFullDetailsByEmail(Email, ref status);

            DataTable dt_Members = ds0.Tables[0];
            DataTable dt_ChildrenInfo = ds0.Tables[1];
            DataTable dt_MembershipOrder = ds0.Tables[2];


            Entities.Members objMembers = new Entities.Members();
            List<Entities.ChildrenInfo> lstChildrenInfo = new List<Entities.ChildrenInfo>();
            List<Entities.MembershipOrders> lstMembershipOrder = new List<Entities.MembershipOrders>(); 

            if (dt_Members.Rows.Count == 1)
            {

                objMembers.MemberId = Convert.ToInt64(dt_Members.Rows[0]["MemberId"]);
                objMembers.ChapterId = (dt_Members.Rows[0]["ChapterId"] != DBNull.Value ? Convert.ToInt64(dt_Members.Rows[0]["ChapterId"]) : 0);
                objMembers.Title = (dt_Members.Rows[0]["Title"] != DBNull.Value ? dt_Members.Rows[0]["Title"].ToString() : null);
                objMembers.Email = dt_Members.Rows[0]["Email"].ToString();
                objMembers.FirstName = dt_Members.Rows[0]["FirstName"].ToString();
                objMembers.LastName = dt_Members.Rows[0]["LastName"].ToString();
                objMembers.ChapterName = (dt_Members.Rows[0]["CHAPTERname"] != DBNull.Value ? dt_Members.Rows[0]["CHAPTERname"].ToString() : null);

                objMembers.ProfileImage = (dt_Members.Rows[0]["ProfileImage"] != DBNull.Value ? dt_Members.Rows[0]["ProfileImage"].ToString() : null);
                objMembers.Occupation = (dt_Members.Rows[0]["Occupation"] != DBNull.Value ? dt_Members.Rows[0]["Occupation"].ToString() : null);
                objMembers.MemberAge = (dt_Members.Rows[0]["MemberAge"] != DBNull.Value ? dt_Members.Rows[0]["MemberAge"].ToString() : null);
                objMembers.MemberSkils = (dt_Members.Rows[0]["MemberSkils"] != DBNull.Value ? dt_Members.Rows[0]["MemberSkils"].ToString() : null);
                objMembers.SpouseSkils = (dt_Members.Rows[0]["SpouseSkils"] != DBNull.Value ? dt_Members.Rows[0]["SpouseSkils"].ToString() : null);
                objMembers.SpouseFirstName = (dt_Members.Rows[0]["SpouseFirstName"] != DBNull.Value ? dt_Members.Rows[0]["SpouseFirstName"].ToString() : null);
                objMembers.SpouseLastName = (dt_Members.Rows[0]["SpouseLastName"] != DBNull.Value ? dt_Members.Rows[0]["SpouseLastName"].ToString() : null);
                objMembers.SpouseOccupation = (dt_Members.Rows[0]["SpouseOccupation"] != DBNull.Value ? dt_Members.Rows[0]["SpouseOccupation"].ToString() : null);
                objMembers.SpouseEmail = (dt_Members.Rows[0]["SpouseEmail"] != DBNull.Value ? dt_Members.Rows[0]["SpouseEmail"].ToString() : null);
                objMembers.SpouseCell = (dt_Members.Rows[0]["SpouseCell"] != DBNull.Value ? dt_Members.Rows[0]["SpouseCell"].ToString() : null);
                objMembers.Address = (dt_Members.Rows[0]["Address"] != DBNull.Value ? dt_Members.Rows[0]["Address"].ToString() : null);

                objMembers.City = (dt_Members.Rows[0]["City"] != DBNull.Value ? dt_Members.Rows[0]["City"].ToString() : null);
                objMembers.State = (dt_Members.Rows[0]["State"] != DBNull.Value ? dt_Members.Rows[0]["State"].ToString() : null);
                objMembers.ZipCode = (dt_Members.Rows[0]["ZipCode"] != DBNull.Value ? dt_Members.Rows[0]["ZipCode"].ToString() : null);
                objMembers.HomePhone = (dt_Members.Rows[0]["HomePhone"] != DBNull.Value ? dt_Members.Rows[0]["HomePhone"].ToString() : null);
                objMembers.MobilePhone = (dt_Members.Rows[0]["MobilePhone"] != DBNull.Value ? dt_Members.Rows[0]["MobilePhone"].ToString() : null);
                objMembers.IsApproved = Convert.ToBoolean(dt_Members.Rows[0]["IsApproved"]);
                objMembers.IsLockedOut = (dt_Members.Rows[0]["IsLockedOut"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsLockedOut"]) : false);
                objMembers.IsActivated = (dt_Members.Rows[0]["IsActivated"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsActivated"]) : false);
                objMembers.DateActivated = (dt_Members.Rows[0]["DateActivated"] != DBNull.Value ? Convert.ToDateTime(dt_Members.Rows[0]["DateActivated"]) : DateTime.MinValue);
                objMembers.MembershipTypeId = (dt_Members.Rows[0]["MembershipTypeId"] != DBNull.Value ? Convert.ToInt64(dt_Members.Rows[0]["MembershipTypeId"]) : 0);
                objMembers.MembershipType = dt_Members.Rows[0]["MembershipType"].ToString();
                objMembers.IsVolunteer = (dt_Members.Rows[0]["IsVolunteer"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsVolunteer"]) : false);
                objMembers.MemberAmount = (dt_Members.Rows[0]["MemberAmount"] != DBNull.Value ? Convert.ToDecimal(dt_Members.Rows[0]["MemberAmount"]) : 0);
                objMembers.IsTeluguorigin = (dt_Members.Rows[0]["IsTeluguorigin"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsTeluguorigin"]) : false);
                objMembers.Comments = (dt_Members.Rows[0]["Comments"] != DBNull.Value ? dt_Members.Rows[0]["Comments"].ToString() : null);
                objMembers.ReferredBy = (dt_Members.Rows[0]["ReferredBy"] != DBNull.Value ? dt_Members.Rows[0]["ReferredBy"].ToString() : null);
                objMembers.MobilePhone = (dt_Members.Rows[0]["MobilePhone"] != DBNull.Value ? dt_Members.Rows[0]["MobilePhone"].ToString() : null);
                objMembers.InsertedTime = Convert.ToDateTime(dt_Members.Rows[0]["InsertedTime"]);
                objMembers.UpdatedTime = Convert.ToDateTime(dt_Members.Rows[0]["UpdatedTime"]);
                objMembers.Fax = (dt_Members.Rows[0]["Fax"] != DBNull.Value ? dt_Members.Rows[0]["Fax"].ToString() : null);
                objMembers.WebsiteAddress = (dt_Members.Rows[0]["WebsiteAddress"] != DBNull.Value ? dt_Members.Rows[0]["WebsiteAddress"].ToString() : null);
                objMembers.Address2 = (dt_Members.Rows[0]["Address2"] != DBNull.Value ? dt_Members.Rows[0]["Address2"].ToString() : null);
                objMembers.ExpiryDate = (dt_Members.Rows[0]["ExpiryDate"] != DBNull.Value ? Convert.ToDateTime(dt_Members.Rows[0]["ExpiryDate"]) : DateTime.MinValue);
                objMembers.UserCount = (dt_Members.Rows[0]["UserCount"] != DBNull.Value ? Convert.ToInt64(dt_Members.Rows[0]["UserCount"]) : 0);

                objMembers.SelfCity = (dt_Members.Rows[0]["SelfCity"] != DBNull.Value ? dt_Members.Rows[0]["SelfCity"].ToString() : null);
                objMembers.SelfDistrict = (dt_Members.Rows[0]["SelfDistrict"] != DBNull.Value ? dt_Members.Rows[0]["SelfDistrict"].ToString() : null);
                objMembers.SelfPhoneNo = (dt_Members.Rows[0]["SelfPhoneNo"] != DBNull.Value ? dt_Members.Rows[0]["SelfPhoneNo"].ToString() : null); 
                objMembers.SelfName = (dt_Members.Rows[0]["SelfName"] != DBNull.Value ? dt_Members.Rows[0]["SelfName"].ToString() : null);
                objMembers.SelfRelation = (dt_Members.Rows[0]["SelfRelation"] != DBNull.Value ? dt_Members.Rows[0]["SelfRelation"].ToString() : null);
                objMembers.SpouseCity = (dt_Members.Rows[0]["SpouseCity"] != DBNull.Value ? dt_Members.Rows[0]["SpouseCity"].ToString() : null);
                objMembers.SpousePhoneNo = (dt_Members.Rows[0]["SpousePhoneNo"] != DBNull.Value ? dt_Members.Rows[0]["SpousePhoneNo"].ToString() : null);
                objMembers.SpouseName = (dt_Members.Rows[0]["SpouseName"] != DBNull.Value ? dt_Members.Rows[0]["SpouseName"].ToString() : null);
                objMembers.SpouseRelation = (dt_Members.Rows[0]["SpouseRelation"] != DBNull.Value ? dt_Members.Rows[0]["SpouseRelation"].ToString() : null);
                objMembers.SpouseDistrict = (dt_Members.Rows[0]["SpouseDistrict"] != DBNull.Value ? dt_Members.Rows[0]["SpouseDistrict"].ToString() : null);
                objMembers.ChapterName = (dt_Members.Rows[0]["CHAPTERname"] != DBNull.Value ? dt_Members.Rows[0]["CHAPTERname"].ToString() : null);

                objMembers.MBachelors = (dt_Members.Rows[0]["MBachelors"] != DBNull.Value ? dt_Members.Rows[0]["MBachelors"].ToString() : null);
                objMembers.MAdvanced = (dt_Members.Rows[0]["MAdvanced"] != DBNull.Value ? dt_Members.Rows[0]["MAdvanced"].ToString() : null);
                objMembers.SPouseBachelors = (dt_Members.Rows[0]["SPouseBachelors"] != DBNull.Value ? dt_Members.Rows[0]["SPouseBachelors"].ToString() : null);
                objMembers.SPouseAdvanced = (dt_Members.Rows[0]["SPouseAdvanced"] != DBNull.Value ? dt_Members.Rows[0]["SPouseAdvanced"].ToString() : null);
            }

            if (dt_ChildrenInfo.Rows.Count != 0)
            {

                foreach (DataRow dr in dt_ChildrenInfo.Rows)
                {
                    Entities.ChildrenInfo objChildrenInfo = new Entities.ChildrenInfo();

                    objChildrenInfo.ChildrenInfoId = Convert.ToInt64(dr["ChildrenInfoId"]);
                    objChildrenInfo.MemberId = Convert.ToInt64(dr["MemberId"]);
                    objChildrenInfo.FirstName = dr["FirstName"].ToString();
                    objChildrenInfo.LastName = dr["LastName"].ToString();
                    objChildrenInfo.Age = (dr["Age"] != DBNull.Value ? Convert.ToInt32(dr["Age"].ToString()) : 0);
                    objChildrenInfo.Relationship = dr["Relationship"].ToString();
                    objChildrenInfo.Email = (dr["Email"] != DBNull.Value ? dr["Email"].ToString() : null);

                    lstChildrenInfo.Add(objChildrenInfo);
                }
            }

            objMembers.lstChildrenInfo = lstChildrenInfo;

            if (dt_MembershipOrder.Rows.Count == 1)
            {
                objMembers.objMembershipOrder.MembershipOrderId = Convert.ToInt64(dt_MembershipOrder.Rows[0]["MembershipOrderId"]);
                objMembers.objMembershipOrder.MemberId = (dt_MembershipOrder.Rows[0]["MemberId"] != DBNull.Value ? Convert.ToInt64(dt_MembershipOrder.Rows[0]["MemberId"]) : 0);
                objMembers.objMembershipOrder.MembershipTypeId = (dt_MembershipOrder.Rows[0]["MembershipTypeId"] != DBNull.Value ? Convert.ToInt64(dt_MembershipOrder.Rows[0]["MembershipTypeId"]) : 0);
                objMembers.objMembershipOrder.Amount = (dt_MembershipOrder.Rows[0]["Amount"] != DBNull.Value ? Convert.ToDecimal(dt_MembershipOrder.Rows[0]["Amount"]) : 0);
                objMembers.objMembershipOrder.TransactionId = (dt_MembershipOrder.Rows[0]["TransactionId"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["TransactionId"].ToString() : null);
                objMembers.objMembershipOrder.PaymentStatusId = (dt_MembershipOrder.Rows[0]["PaymentStatusId"] != DBNull.Value ? Convert.ToInt64(dt_MembershipOrder.Rows[0]["PaymentStatusId"]) : 0);
                objMembers.objMembershipOrder.PaymentMethodId = (dt_MembershipOrder.Rows[0]["PaymentMethodId"] != DBNull.Value ? Convert.ToInt64(dt_MembershipOrder.Rows[0]["PaymentMethodId"]) : 0);
                objMembers.objMembershipOrder.PaymentBy = (dt_MembershipOrder.Rows[0]["PaymentBy"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["PaymentBy"].ToString() : null);
                objMembers.objMembershipOrder.AdminComment = (dt_MembershipOrder.Rows[0]["AdminComment"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["AdminComment"].ToString() : null);
                objMembers.objMembershipOrder.PaymentStatus = (dt_MembershipOrder.Rows[0]["PaymentStatus"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["PaymentStatus"].ToString() : null);
                objMembers.objMembershipOrder.PaymentMethod = (dt_MembershipOrder.Rows[0]["PaymentMethod"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["PaymentMethod"].ToString() : null);
                objMembers.objMembershipOrder.MembershipType = (dt_MembershipOrder.Rows[0]["MembershipType"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["MembershipType"].ToString() : null);
                objMembers.objMembershipOrder.UserComment = (dt_MembershipOrder.Rows[0]["UserComment"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["UserComment"].ToString() : null);
                objMembers.objMembershipOrder.OrderDate = (dt_MembershipOrder.Rows[0]["OrderDate"] != DBNull.Value ? Convert.ToDateTime(dt_MembershipOrder.Rows[0]["OrderDate"]) : DateTime.MinValue);
                objMembers.objMembershipOrder.ExpiryDate = (dt_MembershipOrder.Rows[0]["ExpiryDate"] != DBNull.Value ? Convert.ToDateTime(dt_MembershipOrder.Rows[0]["ExpiryDate"]) : DateTime.MinValue);
                objMembers.objMembershipOrder.Expiry = (dt_MembershipOrder.Rows[0]["Expiry"] != DBNull.Value ? Convert.ToInt32(dt_MembershipOrder.Rows[0]["Expiry"]) : 0);
                //objMembers.objMembershipOrder.UpdatedTime = Convert.ToDateTime(dt_MembershipOrder.Rows[0]["UpdatedTime"]);
               // objMembers.objMembershipOrder.UpdatedBy = dt_MembershipOrder.Rows[0]["UpdatedBy"].ToString();

            }
            if (ds0.Tables.Count > 3 && ds0.Tables[3].Rows.Count > 0)
            {
                DataRow dr = ds0.Tables[3].Rows[0];

                objMembers.SubscriptionId = dr["SubscriotionId"] != DBNull.Value ? dr["SubscriotionId"].ToString() : null;
                objMembers.RecurringType = dr["RecurringType"] != DBNull.Value ? dr["RecurringType"].ToString() : null;
                           
            }
            objMembers.lstMembershipOrder = lstMembershipOrder;

            return objMembers;
        }

        public Entities.Members GetMemberFullDetailsByPhoneNo(string MobilePhone, ref int status)
        {
            DataSet ds0 = _Members1.GetMemberFullDetailsByPhoneNo(MobilePhone, ref status);

            DataTable dt_Members = ds0.Tables[0];
            DataTable dt_ChildrenInfo = ds0.Tables[1];
            DataTable dt_MembershipOrder = ds0.Tables[2];


            Entities.Members objMembers = new Entities.Members();
            List<Entities.ChildrenInfo> lstChildrenInfo = new List<Entities.ChildrenInfo>();
            List<Entities.MembershipOrders> lstMembershipOrder = new List<Entities.MembershipOrders>();

            if (dt_Members.Rows.Count == 1)
            {

                objMembers.MemberId = Convert.ToInt64(dt_Members.Rows[0]["MemberId"]);
                objMembers.ChapterId = (dt_Members.Rows[0]["ChapterId"] != DBNull.Value ? Convert.ToInt64(dt_Members.Rows[0]["ChapterId"]) : 0);
                objMembers.Title = (dt_Members.Rows[0]["Title"] != DBNull.Value ? dt_Members.Rows[0]["Title"].ToString() : null);
                objMembers.Email = dt_Members.Rows[0]["Email"].ToString();
                objMembers.FirstName = dt_Members.Rows[0]["FirstName"].ToString();
                objMembers.LastName = dt_Members.Rows[0]["LastName"].ToString();
                objMembers.ProfileImage = (dt_Members.Rows[0]["ProfileImage"] != DBNull.Value ? dt_Members.Rows[0]["ProfileImage"].ToString() : null);
                objMembers.Occupation = (dt_Members.Rows[0]["Occupation"] != DBNull.Value ? dt_Members.Rows[0]["Occupation"].ToString() : null);
                objMembers.MemberAge = (dt_Members.Rows[0]["MemberAge"] != DBNull.Value ? dt_Members.Rows[0]["MemberAge"].ToString() : null);
                objMembers.MemberSkils = (dt_Members.Rows[0]["Occupation"] != DBNull.Value ? dt_Members.Rows[0]["MemberSkils"].ToString() : null);
                objMembers.SpouseSkils = (dt_Members.Rows[0]["SpouseSkils"] != DBNull.Value ? dt_Members.Rows[0]["SpouseSkils"].ToString() : null);
                objMembers.SpouseFirstName = (dt_Members.Rows[0]["SpouseFirstName"] != DBNull.Value ? dt_Members.Rows[0]["SpouseFirstName"].ToString() : null);
                objMembers.SpouseLastName = (dt_Members.Rows[0]["SpouseLastName"] != DBNull.Value ? dt_Members.Rows[0]["SpouseLastName"].ToString() : null);
                objMembers.SpouseOccupation = (dt_Members.Rows[0]["SpouseOccupation"] != DBNull.Value ? dt_Members.Rows[0]["SpouseOccupation"].ToString() : null);
                objMembers.SpouseEmail = (dt_Members.Rows[0]["SpouseEmail"] != DBNull.Value ? dt_Members.Rows[0]["SpouseEmail"].ToString() : null);
                objMembers.SpouseCell = (dt_Members.Rows[0]["SpouseCell"] != DBNull.Value ? dt_Members.Rows[0]["SpouseCell"].ToString() : null);
                objMembers.Address = (dt_Members.Rows[0]["Address"] != DBNull.Value ? dt_Members.Rows[0]["Address"].ToString() : null);
                objMembers.City = (dt_Members.Rows[0]["City"] != DBNull.Value ? dt_Members.Rows[0]["City"].ToString() : null);
                objMembers.State = (dt_Members.Rows[0]["State"] != DBNull.Value ? dt_Members.Rows[0]["State"].ToString() : null);
                objMembers.ZipCode = (dt_Members.Rows[0]["ZipCode"] != DBNull.Value ? dt_Members.Rows[0]["ZipCode"].ToString() : null);
                objMembers.HomePhone = (dt_Members.Rows[0]["HomePhone"] != DBNull.Value ? dt_Members.Rows[0]["HomePhone"].ToString() : null);
                objMembers.MobilePhone = (dt_Members.Rows[0]["MobilePhone"] != DBNull.Value ? dt_Members.Rows[0]["MobilePhone"].ToString() : null);
                objMembers.IsApproved = Convert.ToBoolean(dt_Members.Rows[0]["IsApproved"]);
                objMembers.IsLockedOut = (dt_Members.Rows[0]["IsLockedOut"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsLockedOut"]) : false);
                objMembers.IsActivated = (dt_Members.Rows[0]["IsActivated"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsActivated"]) : false);
                objMembers.DateActivated = (dt_Members.Rows[0]["DateActivated"] != DBNull.Value ? Convert.ToDateTime(dt_Members.Rows[0]["DateActivated"]) : DateTime.MinValue);
                objMembers.MembershipTypeId = (dt_Members.Rows[0]["MembershipTypeId"] != DBNull.Value ? Convert.ToInt64(dt_Members.Rows[0]["MembershipTypeId"]) : 0);
                objMembers.MembershipType = dt_Members.Rows[0]["MembershipType"].ToString();
                objMembers.IsVolunteer = (dt_Members.Rows[0]["IsVolunteer"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsVolunteer"]) : false);
                objMembers.MemberAmount = (dt_Members.Rows[0]["MemberAmount"] != DBNull.Value ? Convert.ToDecimal(dt_Members.Rows[0]["MemberAmount"]) : 0);
                objMembers.IsTeluguorigin = (dt_Members.Rows[0]["IsTeluguorigin"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsTeluguorigin"]) : false);
                objMembers.Comments = (dt_Members.Rows[0]["Comments"] != DBNull.Value ? dt_Members.Rows[0]["Comments"].ToString() : null);
                objMembers.ReferredBy = (dt_Members.Rows[0]["ReferredBy"] != DBNull.Value ? dt_Members.Rows[0]["ReferredBy"].ToString() : null);
                objMembers.MobilePhone = (dt_Members.Rows[0]["MobilePhone"] != DBNull.Value ? dt_Members.Rows[0]["MobilePhone"].ToString() : null);
                objMembers.InsertedTime = Convert.ToDateTime(dt_Members.Rows[0]["InsertedTime"]);
                objMembers.UpdatedTime = Convert.ToDateTime(dt_Members.Rows[0]["UpdatedTime"]);
                objMembers.Fax = (dt_Members.Rows[0]["Fax"] != DBNull.Value ? dt_Members.Rows[0]["Fax"].ToString() : null);
                objMembers.WebsiteAddress = (dt_Members.Rows[0]["WebsiteAddress"] != DBNull.Value ? dt_Members.Rows[0]["WebsiteAddress"].ToString() : null);
                objMembers.Address2 = (dt_Members.Rows[0]["Address2"] != DBNull.Value ? dt_Members.Rows[0]["Address2"].ToString() : null);
                objMembers.ExpiryDate = (dt_Members.Rows[0]["ExpiryDate"] != DBNull.Value ? Convert.ToDateTime(dt_Members.Rows[0]["ExpiryDate"]) : DateTime.MinValue);
            }

            if (dt_ChildrenInfo.Rows.Count != 0)
            {

                foreach (DataRow dr in dt_ChildrenInfo.Rows)
                {
                    Entities.ChildrenInfo objChildrenInfo = new Entities.ChildrenInfo();

                    objChildrenInfo.ChildrenInfoId = Convert.ToInt64(dr["ChildrenInfoId"]);
                    objChildrenInfo.MemberId = Convert.ToInt64(dr["MemberId"]);
                    objChildrenInfo.FirstName = dr["FirstName"].ToString();
                    objChildrenInfo.LastName = dr["LastName"].ToString();
                    objChildrenInfo.Age = (dr["Age"] != DBNull.Value ? Convert.ToInt32(dr["Age"].ToString()) : 0);
                    objChildrenInfo.Relationship = dr["Relationship"].ToString();

                    lstChildrenInfo.Add(objChildrenInfo);
                }
            }

            objMembers.lstChildrenInfo = lstChildrenInfo;

            if (dt_MembershipOrder.Rows.Count == 1)
            {
                objMembers.objMembershipOrder.MembershipOrderId = Convert.ToInt64(dt_MembershipOrder.Rows[0]["MembershipOrderId"]);
                objMembers.objMembershipOrder.MemberId = (dt_MembershipOrder.Rows[0]["MemberId"] != DBNull.Value ? Convert.ToInt64(dt_MembershipOrder.Rows[0]["MemberId"]) : 0);
                objMembers.objMembershipOrder.MembershipTypeId = (dt_MembershipOrder.Rows[0]["MembershipTypeId"] != DBNull.Value ? Convert.ToInt64(dt_MembershipOrder.Rows[0]["MembershipTypeId"]) : 0);
                objMembers.objMembershipOrder.Amount = (dt_MembershipOrder.Rows[0]["Amount"] != DBNull.Value ? Convert.ToDecimal(dt_MembershipOrder.Rows[0]["Amount"]) : 0);
                objMembers.objMembershipOrder.TransactionId = (dt_MembershipOrder.Rows[0]["TransactionId"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["TransactionId"].ToString() : null);
                objMembers.objMembershipOrder.PaymentStatusId = (dt_MembershipOrder.Rows[0]["PaymentStatusId"] != DBNull.Value ? Convert.ToInt64(dt_MembershipOrder.Rows[0]["PaymentStatusId"]) : 0);
                objMembers.objMembershipOrder.PaymentMethodId = (dt_MembershipOrder.Rows[0]["PaymentMethodId"] != DBNull.Value ? Convert.ToInt64(dt_MembershipOrder.Rows[0]["PaymentMethodId"]) : 0);
                objMembers.objMembershipOrder.PaymentBy = (dt_MembershipOrder.Rows[0]["PaymentBy"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["PaymentBy"].ToString() : null);
                objMembers.objMembershipOrder.AdminComment = (dt_MembershipOrder.Rows[0]["AdminComment"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["AdminComment"].ToString() : null);
                objMembers.objMembershipOrder.PaymentStatus = (dt_MembershipOrder.Rows[0]["PaymentStatus"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["PaymentStatus"].ToString() : null);
                objMembers.objMembershipOrder.PaymentMethod = (dt_MembershipOrder.Rows[0]["PaymentMethod"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["PaymentMethod"].ToString() : null);
                objMembers.objMembershipOrder.MembershipType = (dt_MembershipOrder.Rows[0]["MembershipType"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["MembershipType"].ToString() : null);
                objMembers.objMembershipOrder.UserComment = (dt_MembershipOrder.Rows[0]["UserComment"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["UserComment"].ToString() : null);
                objMembers.objMembershipOrder.OrderDate = (dt_MembershipOrder.Rows[0]["OrderDate"] != DBNull.Value ? Convert.ToDateTime(dt_MembershipOrder.Rows[0]["OrderDate"]) : DateTime.MinValue);
                objMembers.objMembershipOrder.ExpiryDate = (dt_MembershipOrder.Rows[0]["ExpiryDate"] != DBNull.Value ? Convert.ToDateTime(dt_MembershipOrder.Rows[0]["ExpiryDate"]) : DateTime.MinValue);
                objMembers.objMembershipOrder.Expiry = (dt_MembershipOrder.Rows[0]["Expiry"] != DBNull.Value ? Convert.ToInt32(dt_MembershipOrder.Rows[0]["Expiry"]) : 0);
                objMembers.objMembershipOrder.UpdatedTime = Convert.ToDateTime(dt_MembershipOrder.Rows[0]["UpdatedTime"]);
                objMembers.objMembershipOrder.UpdatedBy = dt_MembershipOrder.Rows[0]["UpdatedBy"].ToString();

            }

            objMembers.lstMembershipOrder = lstMembershipOrder;

            return objMembers;
        }

        public Entities.Members FEGetMemberFullDetailsByEmail(string Email, ref int status)
        {
            DataSet ds0 = _Members1.FEGetMemberFullDetailsByEmail(Email, ref status);

            DataTable dt_Members = ds0.Tables[0];
            DataTable dt_ChildrenInfo = ds0.Tables[1];
            DataTable dt_MembershipOrder = ds0.Tables[2];


            Entities.Members objMembers = new Entities.Members();
            List<Entities.ChildrenInfo> lstChildrenInfo = new List<Entities.ChildrenInfo>();
            List<Entities.MembershipOrders> lstMembershipOrder = new List<Entities.MembershipOrders>();

            if (dt_Members.Rows.Count == 1)
            {

                objMembers.MemberId = Convert.ToInt64(dt_Members.Rows[0]["MemberId"]);
                objMembers.ChapterId = (dt_Members.Rows[0]["ChapterId"] != DBNull.Value ? Convert.ToInt64(dt_Members.Rows[0]["ChapterId"]) : 0);
                objMembers.Title = (dt_Members.Rows[0]["Title"] != DBNull.Value ? dt_Members.Rows[0]["Title"].ToString() : null);
                objMembers.Email = dt_Members.Rows[0]["Email"].ToString();
                objMembers.FirstName = dt_Members.Rows[0]["FirstName"].ToString();
                objMembers.LastName = dt_Members.Rows[0]["LastName"].ToString();
                objMembers.ProfileImage = (dt_Members.Rows[0]["ProfileImage"] != DBNull.Value ? dt_Members.Rows[0]["ProfileImage"].ToString() : null);
                objMembers.Occupation = (dt_Members.Rows[0]["Occupation"] != DBNull.Value ? dt_Members.Rows[0]["Occupation"].ToString() : null);
                objMembers.MemberAge = (dt_Members.Rows[0]["MemberAge"] != DBNull.Value ? dt_Members.Rows[0]["MemberAge"].ToString() : null);
                objMembers.MemberSkils = (dt_Members.Rows[0]["Occupation"] != DBNull.Value ? dt_Members.Rows[0]["MemberSkils"].ToString() : null);
                objMembers.SpouseSkils = (dt_Members.Rows[0]["SpouseSkils"] != DBNull.Value ? dt_Members.Rows[0]["SpouseSkils"].ToString() : null);
                objMembers.SpouseFirstName = (dt_Members.Rows[0]["SpouseFirstName"] != DBNull.Value ? dt_Members.Rows[0]["SpouseFirstName"].ToString() : null);
                objMembers.SpouseLastName = (dt_Members.Rows[0]["SpouseLastName"] != DBNull.Value ? dt_Members.Rows[0]["SpouseLastName"].ToString() : null);
                objMembers.SpouseOccupation = (dt_Members.Rows[0]["SpouseOccupation"] != DBNull.Value ? dt_Members.Rows[0]["SpouseOccupation"].ToString() : null);
                objMembers.SpouseEmail = (dt_Members.Rows[0]["SpouseEmail"] != DBNull.Value ? dt_Members.Rows[0]["SpouseEmail"].ToString() : null);
                objMembers.SpouseCell = (dt_Members.Rows[0]["SpouseCell"] != DBNull.Value ? dt_Members.Rows[0]["SpouseCell"].ToString() : null);
                objMembers.Address = (dt_Members.Rows[0]["Address"] != DBNull.Value ? dt_Members.Rows[0]["Address"].ToString() : null);
                objMembers.City = (dt_Members.Rows[0]["City"] != DBNull.Value ? dt_Members.Rows[0]["City"].ToString() : null);
                objMembers.State = (dt_Members.Rows[0]["State"] != DBNull.Value ? dt_Members.Rows[0]["State"].ToString() : null);
                objMembers.ZipCode = (dt_Members.Rows[0]["ZipCode"] != DBNull.Value ? dt_Members.Rows[0]["ZipCode"].ToString() : null);
                objMembers.HomePhone = (dt_Members.Rows[0]["HomePhone"] != DBNull.Value ? dt_Members.Rows[0]["HomePhone"].ToString() : null);
                objMembers.MobilePhone = (dt_Members.Rows[0]["MobilePhone"] != DBNull.Value ? dt_Members.Rows[0]["MobilePhone"].ToString() : null);
                objMembers.IsApproved = Convert.ToBoolean(dt_Members.Rows[0]["IsApproved"]);
                objMembers.IsLockedOut = (dt_Members.Rows[0]["IsLockedOut"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsLockedOut"]) : false);
                objMembers.IsActivated = (dt_Members.Rows[0]["IsActivated"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsActivated"]) : false);
                objMembers.DateActivated = (dt_Members.Rows[0]["DateActivated"] != DBNull.Value ? Convert.ToDateTime(dt_Members.Rows[0]["DateActivated"]) : DateTime.MinValue);
                objMembers.MembershipTypeId = (dt_Members.Rows[0]["MembershipTypeId"] != DBNull.Value ? Convert.ToInt64(dt_Members.Rows[0]["MembershipTypeId"]) : 0);
                objMembers.MembershipType = dt_Members.Rows[0]["MembershipType"].ToString();
                objMembers.IsVolunteer = (dt_Members.Rows[0]["IsVolunteer"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsVolunteer"]) : false);
                objMembers.MemberAmount = (dt_Members.Rows[0]["MemberAmount"] != DBNull.Value ? Convert.ToDecimal(dt_Members.Rows[0]["MemberAmount"]) : 0);
                objMembers.IsTeluguorigin = (dt_Members.Rows[0]["IsTeluguorigin"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsTeluguorigin"]) : false);
                objMembers.Comments = (dt_Members.Rows[0]["Comments"] != DBNull.Value ? dt_Members.Rows[0]["Comments"].ToString() : null);
                objMembers.ReferredBy = (dt_Members.Rows[0]["ReferredBy"] != DBNull.Value ? dt_Members.Rows[0]["ReferredBy"].ToString() : null);
                objMembers.MobilePhone = (dt_Members.Rows[0]["MobilePhone"] != DBNull.Value ? dt_Members.Rows[0]["MobilePhone"].ToString() : null);
                objMembers.InsertedTime = Convert.ToDateTime(dt_Members.Rows[0]["InsertedTime"]);
                objMembers.UpdatedTime = Convert.ToDateTime(dt_Members.Rows[0]["UpdatedTime"]);
                objMembers.Fax = (dt_Members.Rows[0]["Fax"] != DBNull.Value ? dt_Members.Rows[0]["Fax"].ToString() : null);
                objMembers.WebsiteAddress = (dt_Members.Rows[0]["WebsiteAddress"] != DBNull.Value ? dt_Members.Rows[0]["WebsiteAddress"].ToString() : null);
                objMembers.Address2 = (dt_Members.Rows[0]["Address2"] != DBNull.Value ? dt_Members.Rows[0]["Address2"].ToString() : null);
                objMembers.ExpiryDate = (dt_Members.Rows[0]["ExpiryDate"] != DBNull.Value ? Convert.ToDateTime(dt_Members.Rows[0]["ExpiryDate"]) : DateTime.MinValue);
            }

            if (dt_ChildrenInfo.Rows.Count != 0)
            {

                foreach (DataRow dr in dt_ChildrenInfo.Rows)
                {
                    Entities.ChildrenInfo objChildrenInfo = new Entities.ChildrenInfo();

                    objChildrenInfo.ChildrenInfoId = Convert.ToInt64(dr["ChildrenInfoId"]);
                    objChildrenInfo.MemberId = Convert.ToInt64(dr["MemberId"]);
                    objChildrenInfo.FirstName = dr["FirstName"].ToString();
                    objChildrenInfo.LastName = dr["LastName"].ToString();
                    objChildrenInfo.Age = (dr["Age"] != DBNull.Value ? Convert.ToInt32(dr["Age"].ToString()) : 0);
                    objChildrenInfo.Relationship = dr["Relationship"].ToString();

                    lstChildrenInfo.Add(objChildrenInfo);
                }
            }

            objMembers.lstChildrenInfo = lstChildrenInfo;

            if (dt_MembershipOrder.Rows.Count == 1)
            {
                objMembers.objMembershipOrder.MembershipOrderId = Convert.ToInt64(dt_MembershipOrder.Rows[0]["MembershipOrderId"]);
                objMembers.objMembershipOrder.MemberId = (dt_MembershipOrder.Rows[0]["MemberId"] != DBNull.Value ? Convert.ToInt64(dt_MembershipOrder.Rows[0]["MemberId"]) : 0);
                objMembers.objMembershipOrder.MembershipTypeId = (dt_MembershipOrder.Rows[0]["MembershipTypeId"] != DBNull.Value ? Convert.ToInt64(dt_MembershipOrder.Rows[0]["MembershipTypeId"]) : 0);
                objMembers.objMembershipOrder.Amount = (dt_MembershipOrder.Rows[0]["Amount"] != DBNull.Value ? Convert.ToDecimal(dt_MembershipOrder.Rows[0]["Amount"]) : 0);
                objMembers.objMembershipOrder.TransactionId = (dt_MembershipOrder.Rows[0]["TransactionId"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["TransactionId"].ToString() : null);
                objMembers.objMembershipOrder.PaymentStatusId = (dt_MembershipOrder.Rows[0]["PaymentStatusId"] != DBNull.Value ? Convert.ToInt64(dt_MembershipOrder.Rows[0]["PaymentStatusId"]) : 0);
                objMembers.objMembershipOrder.PaymentMethodId = (dt_MembershipOrder.Rows[0]["PaymentMethodId"] != DBNull.Value ? Convert.ToInt64(dt_MembershipOrder.Rows[0]["PaymentMethodId"]) : 0);
                objMembers.objMembershipOrder.PaymentBy = (dt_MembershipOrder.Rows[0]["PaymentBy"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["PaymentBy"].ToString() : null);
                objMembers.objMembershipOrder.AdminComment = (dt_MembershipOrder.Rows[0]["AdminComment"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["AdminComment"].ToString() : null);
                objMembers.objMembershipOrder.PaymentStatus = (dt_MembershipOrder.Rows[0]["PaymentStatus"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["PaymentStatus"].ToString() : null);
                objMembers.objMembershipOrder.PaymentMethod = (dt_MembershipOrder.Rows[0]["PaymentMethod"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["PaymentMethod"].ToString() : null);
                objMembers.objMembershipOrder.MembershipType = (dt_MembershipOrder.Rows[0]["MembershipType"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["MembershipType"].ToString() : null);
                objMembers.objMembershipOrder.UserComment = (dt_MembershipOrder.Rows[0]["UserComment"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["UserComment"].ToString() : null);
                objMembers.objMembershipOrder.OrderDate = (dt_MembershipOrder.Rows[0]["OrderDate"] != DBNull.Value ? Convert.ToDateTime(dt_MembershipOrder.Rows[0]["OrderDate"]) : DateTime.MinValue);
                objMembers.objMembershipOrder.ExpiryDate = (dt_MembershipOrder.Rows[0]["ExpiryDate"] != DBNull.Value ? Convert.ToDateTime(dt_MembershipOrder.Rows[0]["ExpiryDate"]) : DateTime.MinValue);
                objMembers.objMembershipOrder.Expiry = (dt_MembershipOrder.Rows[0]["Expiry"] != DBNull.Value ? Convert.ToInt32(dt_MembershipOrder.Rows[0]["Expiry"]) : 0);
                objMembers.objMembershipOrder.UpdatedTime = Convert.ToDateTime(dt_MembershipOrder.Rows[0]["UpdatedTime"]);
                objMembers.objMembershipOrder.UpdatedBy = dt_MembershipOrder.Rows[0]["UpdatedBy"].ToString();

            }

            objMembers.lstMembershipOrder = lstMembershipOrder;

            return objMembers;
        }



        public List<Entities.Members> FEGetMembersListByVariable( Int64 MembershipTypeId, string Search, string Sort, int PageNo, int Items, ref int Total)
        {
            DataTable dt = _Members.FEGetMembersListByVariable(MembershipTypeId, Search, Sort, PageNo, Items, ref Total);
            List<Entities.Members> lstMembers = new List<Entities.Members>();

            if (dt.Rows.Count == 0 && PageNo > 1)
            {
                dt = _Members.FEGetMembersListByVariable(MembershipTypeId, Search, Sort, PageNo, Items, ref Total);
            }

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Entities.Members objMembers = new Entities.Members();

                    objMembers.RId = Convert.ToInt64(dr["Rid"]);
                    objMembers.MemberId = Convert.ToInt64(dr["MemberId"]);
                    objMembers.Title = dr["Title"].ToString();
                    objMembers.Email = dr["Email"].ToString();
                    objMembers.FirstName = dr["FirstName"].ToString();
                    objMembers.LastName = dr["LastName"].ToString();
                    objMembers.MemberSkils = (dr["MemberSkils"] != DBNull.Value ? dr["MemberSkils"].ToString() : null);

                    objMembers.ProfileImage = (dr["ProfileImage"] != DBNull.Value ? dr["ProfileImage"].ToString() : null);
                    objMembers.Occupation = (dr["Occupation"] != DBNull.Value ? dr["Occupation"].ToString() : null);
                    objMembers.SpouseFirstName = (dr["SpouseFirstName"] != DBNull.Value ? dr["SpouseFirstName"].ToString() : null);
                    objMembers.SpouseLastName = (dr["SpouseLastName"] != DBNull.Value ? dr["SpouseLastName"].ToString() : null);
                    objMembers.SpouseOccupation = (dr["SpouseOccupation"] != DBNull.Value ? dr["SpouseOccupation"].ToString() : null);
                    objMembers.SpouseEmail = (dr["SpouseEmail"] != DBNull.Value ? dr["SpouseEmail"].ToString() : null);
                    objMembers.SpouseCell = (dr["SpouseCell"] != DBNull.Value ? dr["SpouseCell"].ToString() : null);
                    objMembers.Address = (dr["Address"] != DBNull.Value ? dr["Address"].ToString() : null);
                    objMembers.City = (dr["City"] != DBNull.Value ? dr["City"].ToString() : null);
                    objMembers.State = (dr["State"] != DBNull.Value ? dr["State"].ToString() : null);
                    objMembers.ZipCode = (dr["ZipCode"] != DBNull.Value ? dr["ZipCode"].ToString() : null);
                    objMembers.HomePhone = (dr["HomePhone"] != DBNull.Value ? dr["HomePhone"].ToString() : null);
                    objMembers.MobilePhone = (dr["MobilePhone"] != DBNull.Value ? dr["MobilePhone"].ToString() : null);
                    objMembers.IsApproved = Convert.ToBoolean(dr["IsApproved"]);
                    objMembers.IsLockedOut = (dr["IsLockedOut"] != DBNull.Value ? Convert.ToBoolean(dr["IsLockedOut"]) : false);
                    objMembers.IsActivated = (dr["IsActivated"] != DBNull.Value ? Convert.ToBoolean(dr["IsActivated"]) : false);
                    objMembers.DateActivated = (dr["DateActivated"] != DBNull.Value ? Convert.ToDateTime(dr["DateActivated"]) : DateTime.MinValue);
                    objMembers.MembershipTypeId = (dr["MembershipTypeId"] != DBNull.Value ? Convert.ToInt64(dr["MembershipTypeId"]) : 0);
                    objMembers.MembershipType = dr["MembershipType"].ToString();
                    objMembers.IsVolunteer = (dr["IsVolunteer"] != DBNull.Value ? Convert.ToBoolean(dr["IsVolunteer"]) : false);
                    objMembers.IsTeluguorigin = (dr["IsTeluguorigin"] != DBNull.Value ? Convert.ToBoolean(dr["IsTeluguorigin"]) : false);
                    objMembers.Comments = (dr["Comments"] != DBNull.Value ? dr["Comments"].ToString() : null);
                    objMembers.ReferredBy = (dr["ReferredBy"] != DBNull.Value ? dr["ReferredBy"].ToString() : null);
                    objMembers.MobilePhone = (dr["MobilePhone"] != DBNull.Value ? dr["MobilePhone"].ToString() : null);
                    objMembers.InsertedTime = Convert.ToDateTime(dr["InsertedTime"]);
                    objMembers.UpdatedTime = Convert.ToDateTime(dr["UpdatedTime"]);
                    objMembers.Amount = (dr["Amount"] != DBNull.Value ? Convert.ToDecimal(dr["Amount"]) : 0);
                    objMembers.objMembershipOrder.TransactionId = (dr["TransactionId"] != DBNull.Value ? dr["TransactionId"].ToString() : null);
                    objMembers.objMembershipOrder.MembershipTypeId = (dr["MembershipTypeId"] != DBNull.Value ? Convert.ToInt64(dr["MembershipTypeId"]) : 0);
                    objMembers.objMembershipOrder.PaymentStatusId = (dr["PaymentStatusId"] != DBNull.Value ? Convert.ToInt64(dr["PaymentStatusId"]) : 0);
                    objMembers.objMembershipOrder.PaymentMethodId = (dr["MembershipTypeId"] != DBNull.Value ? Convert.ToInt64(dr["MembershipTypeId"]) : 0);
                    objMembers.objMembershipOrder.MembershipTypeId = (dr["PaymentMethodId"] != DBNull.Value ? Convert.ToInt64(dr["PaymentMethodId"]) : 0);
                    objMembers.objMembershipOrder.AdminComment = (dr["AdminComment"] != DBNull.Value ? dr["AdminComment"].ToString() : null);
                    objMembers.objMembershipOrder.UserComment = (dr["UserComment"] != DBNull.Value ? dr["UserComment"].ToString() : null);
                    objMembers.PaymentStatus = (dr["PaymentStatus"] != DBNull.Value ? dr["PaymentStatus"].ToString() : null);
                    objMembers.objMembershipOrder.PaymentMethod = (dr["PaymentMethod"] != DBNull.Value ? dr["PaymentMethod"].ToString() : null);
                    objMembers.objMembershipOrder.OrderDate = (dr["OrderDate"] != DBNull.Value ? Convert.ToDateTime(dr["OrderDate"]) : DateTime.MinValue);
                    objMembers.objMembershipOrder.ExpiryDate = (dr["ExpiryDate"] != DBNull.Value ? Convert.ToDateTime(dr["ExpiryDate"]) : DateTime.MinValue);
                    objMembers.Fax = (dr["Fax"] != DBNull.Value ? dr["Fax"].ToString() : null);
                    objMembers.WebsiteAddress = (dr["WebsiteAddress"] != DBNull.Value ? dr["WebsiteAddress"].ToString() : null);
                    objMembers.Address2 = (dr["Address2"] != DBNull.Value ? dr["Address2"].ToString() : null);
                    lstMembers.Add(objMembers);
                }
            }
            return lstMembers;
        }

        public Entities.Members FEMemberGetFullDetails(Int64 MemberId, ref int status)
        {
            DataTable dt_Members = _Members1.FEMemberGetFullDetails(MemberId, ref status);

            Entities.Members objMembers = new Entities.Members();
            if (dt_Members.Rows.Count == 1)
            {

                objMembers.MemberId = Convert.ToInt64(dt_Members.Rows[0]["MemberId"]);
                objMembers.ChapterId = (dt_Members.Rows[0]["ChapterId"] != DBNull.Value ? Convert.ToInt64(dt_Members.Rows[0]["ChapterId"]) : 0);
                objMembers.Title = (dt_Members.Rows[0]["Title"] != DBNull.Value ? dt_Members.Rows[0]["Title"].ToString() : null);
                objMembers.Email = dt_Members.Rows[0]["Email"].ToString();
                objMembers.FirstName = dt_Members.Rows[0]["FirstName"].ToString();
                objMembers.LastName = dt_Members.Rows[0]["LastName"].ToString();
                objMembers.ProfileImage = (dt_Members.Rows[0]["ProfileImage"] != DBNull.Value ? dt_Members.Rows[0]["ProfileImage"].ToString() : null);
                objMembers.Occupation = (dt_Members.Rows[0]["Occupation"] != DBNull.Value ? dt_Members.Rows[0]["Occupation"].ToString() : null);
                objMembers.MemberAge = (dt_Members.Rows[0]["MemberAge"] != DBNull.Value ? dt_Members.Rows[0]["MemberAge"].ToString() : null);
                objMembers.MemberSkils = (dt_Members.Rows[0]["Occupation"] != DBNull.Value ? dt_Members.Rows[0]["MemberSkils"].ToString() : null);
                objMembers.SpouseSkils = (dt_Members.Rows[0]["SpouseSkils"] != DBNull.Value ? dt_Members.Rows[0]["SpouseSkils"].ToString() : null);
                objMembers.SpouseFirstName = (dt_Members.Rows[0]["SpouseFirstName"] != DBNull.Value ? dt_Members.Rows[0]["SpouseFirstName"].ToString() : null);
                objMembers.SpouseLastName = (dt_Members.Rows[0]["SpouseLastName"] != DBNull.Value ? dt_Members.Rows[0]["SpouseLastName"].ToString() : null);
                objMembers.SpouseOccupation = (dt_Members.Rows[0]["SpouseOccupation"] != DBNull.Value ? dt_Members.Rows[0]["SpouseOccupation"].ToString() : null);
                objMembers.SpouseEmail = (dt_Members.Rows[0]["SpouseEmail"] != DBNull.Value ? dt_Members.Rows[0]["SpouseEmail"].ToString() : null);
                objMembers.SpouseCell = (dt_Members.Rows[0]["SpouseCell"] != DBNull.Value ? dt_Members.Rows[0]["SpouseCell"].ToString() : null);
                objMembers.Address = (dt_Members.Rows[0]["Address"] != DBNull.Value ? dt_Members.Rows[0]["Address"].ToString() : null);
                objMembers.City = (dt_Members.Rows[0]["City"] != DBNull.Value ? dt_Members.Rows[0]["City"].ToString() : null);
                objMembers.State = (dt_Members.Rows[0]["State"] != DBNull.Value ? dt_Members.Rows[0]["State"].ToString() : null);
                objMembers.ZipCode = (dt_Members.Rows[0]["ZipCode"] != DBNull.Value ? dt_Members.Rows[0]["ZipCode"].ToString() : null);
                objMembers.HomePhone = (dt_Members.Rows[0]["HomePhone"] != DBNull.Value ? dt_Members.Rows[0]["HomePhone"].ToString() : null);
                objMembers.MobilePhone = (dt_Members.Rows[0]["MobilePhone"] != DBNull.Value ? dt_Members.Rows[0]["MobilePhone"].ToString() : null);
                objMembers.IsApproved = Convert.ToBoolean(dt_Members.Rows[0]["IsApproved"]);
                objMembers.IsLockedOut = (dt_Members.Rows[0]["IsLockedOut"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsLockedOut"]) : false);
                objMembers.IsActivated = (dt_Members.Rows[0]["IsActivated"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsActivated"]) : false);
                objMembers.DateActivated = (dt_Members.Rows[0]["DateActivated"] != DBNull.Value ? Convert.ToDateTime(dt_Members.Rows[0]["DateActivated"]) : DateTime.MinValue);
                objMembers.MembershipTypeId = (dt_Members.Rows[0]["MembershipTypeId"] != DBNull.Value ? Convert.ToInt64(dt_Members.Rows[0]["MembershipTypeId"]) : 0);
                objMembers.IsVolunteer = (dt_Members.Rows[0]["IsVolunteer"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsVolunteer"]) : false);
                objMembers.IsTeluguorigin = (dt_Members.Rows[0]["IsTeluguorigin"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsTeluguorigin"]) : false);
                objMembers.Comments = (dt_Members.Rows[0]["Comments"] != DBNull.Value ? dt_Members.Rows[0]["Comments"].ToString() : null);
                objMembers.ReferredBy = (dt_Members.Rows[0]["ReferredBy"] != DBNull.Value ? dt_Members.Rows[0]["ReferredBy"].ToString() : null);
                objMembers.MobilePhone = (dt_Members.Rows[0]["MobilePhone"] != DBNull.Value ? dt_Members.Rows[0]["MobilePhone"].ToString() : null);
                objMembers.InsertedTime = Convert.ToDateTime(dt_Members.Rows[0]["InsertedTime"]);
                objMembers.UpdatedTime = Convert.ToDateTime(dt_Members.Rows[0]["UpdatedTime"]);
                objMembers.Fax = (dt_Members.Rows[0]["Fax"] != DBNull.Value ? dt_Members.Rows[0]["Fax"].ToString() : null);
                objMembers.WebsiteAddress = (dt_Members.Rows[0]["WebsiteAddress"] != DBNull.Value ? dt_Members.Rows[0]["WebsiteAddress"].ToString() : null);
                objMembers.Address2 = (dt_Members.Rows[0]["Address2"] != DBNull.Value ? dt_Members.Rows[0]["Address2"].ToString() : null);
            }

            return objMembers;
        }


        public Entities.Members GetMemberFullDetailsByUserName(string UserName, ref int status)
        {
            DataSet ds0 = _Members1.GetMemberFullDetailsByUserName(UserName, ref status);

            DataTable dt_Members = ds0.Tables[0];
            DataTable dt_ChildrenInfo = ds0.Tables[1];
            DataTable dt_MembershipOrder = ds0.Tables[2];


            Entities.Members objMembers = new Entities.Members();
            List<Entities.ChildrenInfo> lstChildrenInfo = new List<Entities.ChildrenInfo>();
            List<Entities.MembershipOrders> lstMembershipOrder = new List<Entities.MembershipOrders>();

            if (dt_Members.Rows.Count == 1)
            {

                objMembers.MemberId = Convert.ToInt64(dt_Members.Rows[0]["MemberId"]);
                objMembers.UserName = dt_Members.Rows[0]["UserName"].ToString();
                objMembers.Email = dt_Members.Rows[0]["Email"].ToString();
                objMembers.FirstName = dt_Members.Rows[0]["FirstName"].ToString();
                objMembers.LastName = dt_Members.Rows[0]["LastName"].ToString();
                objMembers.ProfileImage = (dt_Members.Rows[0]["ProfileImage"] != DBNull.Value ? dt_Members.Rows[0]["ProfileImage"].ToString() : null);
                objMembers.Occupation = (dt_Members.Rows[0]["Occupation"] != DBNull.Value ? dt_Members.Rows[0]["Occupation"].ToString() : null);
                objMembers.SpouseFirstName = (dt_Members.Rows[0]["SpouseFirstName"] != DBNull.Value ? dt_Members.Rows[0]["SpouseFirstName"].ToString() : null);
                objMembers.SpouseLastName = (dt_Members.Rows[0]["SpouseLastName"] != DBNull.Value ? dt_Members.Rows[0]["SpouseLastName"].ToString() : null);
                objMembers.SpouseOccupation = (dt_Members.Rows[0]["SpouseOccupation"] != DBNull.Value ? dt_Members.Rows[0]["SpouseOccupation"].ToString() : null);
                objMembers.SpouseEmail = (dt_Members.Rows[0]["SpouseEmail"] != DBNull.Value ? dt_Members.Rows[0]["SpouseEmail"].ToString() : null);
                objMembers.SpouseCell = (dt_Members.Rows[0]["SpouseCell"] != DBNull.Value ? dt_Members.Rows[0]["SpouseCell"].ToString() : null);
                objMembers.Address = (dt_Members.Rows[0]["Address"] != DBNull.Value ? dt_Members.Rows[0]["Address"].ToString() : null);
                objMembers.City = (dt_Members.Rows[0]["City"] != DBNull.Value ? dt_Members.Rows[0]["City"].ToString() : null);
                objMembers.State = (dt_Members.Rows[0]["State"] != DBNull.Value ? dt_Members.Rows[0]["State"].ToString() : null);
                objMembers.ZipCode = (dt_Members.Rows[0]["ZipCode"] != DBNull.Value ? dt_Members.Rows[0]["ZipCode"].ToString() : null);
                objMembers.HomePhone = (dt_Members.Rows[0]["HomePhone"] != DBNull.Value ? dt_Members.Rows[0]["HomePhone"].ToString() : null);
                objMembers.MobilePhone = (dt_Members.Rows[0]["MobilePhone"] != DBNull.Value ? dt_Members.Rows[0]["MobilePhone"].ToString() : null);
                objMembers.IsApproved = Convert.ToBoolean(dt_Members.Rows[0]["IsApproved"]);
                objMembers.IsLockedOut = Convert.ToBoolean(dt_Members.Rows[0]["IsLockedOut"]);
                objMembers.IsActivated = Convert.ToBoolean(dt_Members.Rows[0]["IsActivated"]);
                objMembers.DateActivated = (dt_Members.Rows[0]["DateActivated"] != DBNull.Value ? Convert.ToDateTime(dt_Members.Rows[0]["DateActivated"]) : DateTime.MinValue);
                objMembers.MembershipTypeId = Convert.ToInt64(dt_Members.Rows[0]["MembershipTypeId"]);
                objMembers.MembershipType = dt_Members.Rows[0]["MembershipType"].ToString();
                objMembers.IsVolunteer = (dt_Members.Rows[0]["IsVolunteer"] != DBNull.Value ? Convert.ToBoolean(dt_Members.Rows[0]["IsVolunteer"].ToString()) : false);
                objMembers.MemberAmount = (dt_Members.Rows[0]["MemberAmount"] != DBNull.Value ? Convert.ToDecimal(dt_Members.Rows[0]["MemberAmount"]) : 0);
                objMembers.IsTeluguorigin = Convert.ToBoolean(dt_Members.Rows[0]["IsTeluguorigin"]);
                objMembers.Comments = (dt_Members.Rows[0]["Comments"] != DBNull.Value ? dt_Members.Rows[0]["Comments"].ToString() : null);
                objMembers.ReferredBy = (dt_Members.Rows[0]["ReferredBy"] != DBNull.Value ? dt_Members.Rows[0]["ReferredBy"].ToString() : null);
                objMembers.MobilePhone = (dt_Members.Rows[0]["MobilePhone"] != DBNull.Value ? dt_Members.Rows[0]["MobilePhone"].ToString() : null);
                objMembers.InsertedTime = Convert.ToDateTime(dt_Members.Rows[0]["InsertedTime"]);
                objMembers.UpdatedTime = Convert.ToDateTime(dt_Members.Rows[0]["UpdatedTime"]);
                objMembers.Fax = (dt_Members.Rows[0]["Fax"] != DBNull.Value ? dt_Members.Rows[0]["Fax"].ToString() : null);
                objMembers.WebsiteAddress = (dt_Members.Rows[0]["WebsiteAddress"] != DBNull.Value ? dt_Members.Rows[0]["WebsiteAddress"].ToString() : null);
                objMembers.Address2 = (dt_Members.Rows[0]["Address2"] != DBNull.Value ? dt_Members.Rows[0]["Address2"].ToString() : null);
            }

            if (dt_ChildrenInfo.Rows.Count != 0)
            {

                foreach (DataRow dr in dt_ChildrenInfo.Rows)
                {
                    Entities.ChildrenInfo objChildrenInfo = new Entities.ChildrenInfo();

                    objChildrenInfo.ChildrenInfoId = Convert.ToInt64(dr["ChildrenInfoId"]);
                    objChildrenInfo.MemberId = Convert.ToInt64(dr["MemberId"]);
                    objChildrenInfo.FirstName = dr["FirstName"].ToString();
                    objChildrenInfo.LastName = dr["LastName"].ToString();
                    objChildrenInfo.Age = (dr["Age"] != DBNull.Value ? Convert.ToInt32(dr["Age"].ToString()) : 0);
                    objChildrenInfo.Relationship = dr["Relationship"].ToString();

                    lstChildrenInfo.Add(objChildrenInfo);
                }
            }

            objMembers.lstChildrenInfo = lstChildrenInfo;

            if (dt_MembershipOrder.Rows.Count == 1)
            {
                objMembers.objMembershipOrder.MembershipOrderId = Convert.ToInt64(dt_MembershipOrder.Rows[0]["MembershipOrderId"]);
                objMembers.objMembershipOrder.MemberId = (dt_MembershipOrder.Rows[0]["MemberId"] != DBNull.Value ? Convert.ToInt64(dt_MembershipOrder.Rows[0]["MemberId"]) : 0);
                objMembers.objMembershipOrder.MembershipTypeId = (dt_MembershipOrder.Rows[0]["MembershipTypeId"] != DBNull.Value ? Convert.ToInt64(dt_MembershipOrder.Rows[0]["MembershipTypeId"]) : 0);
                objMembers.objMembershipOrder.Amount = (dt_MembershipOrder.Rows[0]["Amount"] != DBNull.Value ? Convert.ToDecimal(dt_MembershipOrder.Rows[0]["Amount"]) : 0);
                objMembers.objMembershipOrder.TransactionId = (dt_MembershipOrder.Rows[0]["TransactionId"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["TransactionId"].ToString() : null);
                objMembers.objMembershipOrder.PaymentStatusId = (dt_MembershipOrder.Rows[0]["PaymentStatusId"] != DBNull.Value ? Convert.ToInt64(dt_MembershipOrder.Rows[0]["PaymentStatusId"]) : 0);
                objMembers.objMembershipOrder.PaymentMethodId = (dt_MembershipOrder.Rows[0]["PaymentMethodId"] != DBNull.Value ? Convert.ToInt64(dt_MembershipOrder.Rows[0]["PaymentMethodId"]) : 0);
                objMembers.objMembershipOrder.PaymentBy = (dt_MembershipOrder.Rows[0]["PaymentBy"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["PaymentBy"].ToString() : null);
                objMembers.objMembershipOrder.AdminComment = (dt_MembershipOrder.Rows[0]["AdminComment"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["AdminComment"].ToString() : null);
                objMembers.objMembershipOrder.PaymentStatus = (dt_MembershipOrder.Rows[0]["PaymentStatus"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["PaymentStatus"].ToString() : null);
                objMembers.objMembershipOrder.PaymentMethod = (dt_MembershipOrder.Rows[0]["PaymentMethod"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["PaymentMethod"].ToString() : null);
                objMembers.objMembershipOrder.MembershipType = (dt_MembershipOrder.Rows[0]["MembershipType"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["MembershipType"].ToString() : null);
                objMembers.objMembershipOrder.UserComment = (dt_MembershipOrder.Rows[0]["UserComment"] != DBNull.Value ? dt_MembershipOrder.Rows[0]["UserComment"].ToString() : null);
                objMembers.objMembershipOrder.OrderDate = (dt_MembershipOrder.Rows[0]["OrderDate"] != DBNull.Value ? Convert.ToDateTime(dt_MembershipOrder.Rows[0]["OrderDate"]) : DateTime.MinValue);
                objMembers.objMembershipOrder.ExpiryDate = (dt_MembershipOrder.Rows[0]["ExpiryDate"] != DBNull.Value ? Convert.ToDateTime(dt_MembershipOrder.Rows[0]["ExpiryDate"]) : DateTime.MinValue);
                objMembers.objMembershipOrder.UpdatedTime = Convert.ToDateTime(dt_MembershipOrder.Rows[0]["UpdatedTime"]);
                objMembers.objMembershipOrder.UpdatedBy = dt_MembershipOrder.Rows[0]["UpdatedBy"].ToString();

            }

            return objMembers;
        }

        public List<Entities.Members> GetMembersListByChapterId(Int64 ChapterId, ref int status)
        {
            DataTable dt = _Members.GetMembersListByChapterId(ChapterId, ref status);
            List<Entities.Members> lstMembers = new List<Entities.Members>();

            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Entities.Members objMembers = new Entities.Members();
                     
                    objMembers.MemberId = Convert.ToInt64(dr["MemberId"]);
                    objMembers.Title = (dr["Title"] != DBNull.Value ? dr["Title"].ToString() : null);
                    objMembers.Email = (dr["Email"] != DBNull.Value ? dr["Email"].ToString() : null);
                    objMembers.FirstName = (dr["FirstName"] != DBNull.Value ? dr["FirstName"].ToString() : null);
                    objMembers.LastName = (dr["LastName"] != DBNull.Value ? dr["LastName"].ToString() : null);
                    objMembers.ProfileImage = (dr["ProfileImage"] != DBNull.Value ? dr["ProfileImage"].ToString() : null);
                    objMembers.Occupation = (dr["Occupation"] != DBNull.Value ? dr["Occupation"].ToString() : null);
                    objMembers.SpouseFirstName = (dr["SpouseFirstName"] != DBNull.Value ? dr["SpouseFirstName"].ToString() : null);
                    objMembers.SpouseLastName = (dr["SpouseLastName"] != DBNull.Value ? dr["SpouseLastName"].ToString() : null);
                    objMembers.SpouseOccupation = (dr["SpouseOccupation"] != DBNull.Value ? dr["SpouseOccupation"].ToString() : null);
                    objMembers.SpouseEmail = (dr["SpouseEmail"] != DBNull.Value ? dr["SpouseEmail"].ToString() : null);
                    objMembers.SpouseCell = (dr["SpouseCell"] != DBNull.Value ? dr["SpouseCell"].ToString() : null);
                    objMembers.Address = (dr["Address"] != DBNull.Value ? dr["Address"].ToString() : null);
                    objMembers.City = (dr["City"] != DBNull.Value ? dr["City"].ToString() : null);
                    objMembers.State = (dr["State"] != DBNull.Value ? dr["State"].ToString() : null);
                    objMembers.ZipCode = (dr["ZipCode"] != DBNull.Value ? dr["ZipCode"].ToString() : null);
                    objMembers.HomePhone = (dr["HomePhone"] != DBNull.Value ? dr["HomePhone"].ToString() : null);
                    objMembers.MobilePhone = (dr["MobilePhone"] != DBNull.Value ? dr["MobilePhone"].ToString() : null);
                    objMembers.IsApproved = (dr["IsApproved"] != DBNull.Value ? Convert.ToBoolean(dr["IsApproved"]) : false);
                    objMembers.IsLockedOut = (dr["IsLockedOut"] != DBNull.Value ? Convert.ToBoolean(dr["IsLockedOut"]):false);
                    objMembers.IsActivated = (dr["IsActivated"] != DBNull.Value ? Convert.ToBoolean(dr["IsActivated"]):false);
                    objMembers.DateActivated = (dr["DateActivated"] != DBNull.Value ? Convert.ToDateTime(dr["DateActivated"]) : DateTime.MinValue);
                    objMembers.MembershipTypeId = (dr["DateActivated"] != DBNull.Value ? Convert.ToInt64(dr["MembershipTypeId"]) : 0);
                    objMembers.MembershipType = (dr["MembershipType"] != DBNull.Value ? dr["MembershipType"].ToString() : "");
                    
                    lstMembers.Add(objMembers);
                }
            }

            return lstMembers;
        }

    

        public Int64 MembersBulkUploads(string ShipmentsXML, ref string Summary, out DataTable dtResults)
        {
            Int64 _status = 0;
            _status = _Members.MembersBulkUploads(ShipmentsXML, ref Summary, out dtResults);
            return _status;
        }
        // ================================================================
        // 1. GetMemberSubscriptionBySubId
        // ================================================================
        public Entities.MemberSubscriptions GetMemberSubscriptionBySubId(
            string subscriptionId, ref int status)
        {
            Entities.MemberSubscriptions objSub = null;
            if (!string.IsNullOrEmpty(subscriptionId))
            {
                objSub = _Members.GetMemberSubscriptionBySubId(subscriptionId, ref status);
            }
            return objSub;
        }
        public Entities.MemberSubscriptions GetMemberSubscriptionByProfileId(
    string profileId, ref int status)
        {
            Entities.MemberSubscriptions objSub = null;
            if (!string.IsNullOrEmpty(profileId))
            {
                objSub = _Members.GetMemberSubscriptionByProfileId(profileId, ref status);
            }
            return objSub;
        }
        // ================================================================
        // 2. UpdateMemberOrderExpiryBySubId
        // ================================================================
        public Int64 UpdateMemberOrderExpiryBySubId(
        Entities.MembershipOrders objMembershipOrders)
        {
            Int64 _status = 0;
            if (objMembershipOrders != null)
            {
                _status = _Members.UpdateMemberOrderExpiryBySubId(objMembershipOrders);
            }
            return _status;
        }
        // ================================================================
        // 3. UpdateSubscriptionNextDate
        // ================================================================

        public Entities.MemberSubscriptions GetMemberSubscriptionByEmail(
    string email, ref int status)
        {
            Entities.MemberSubscriptions objSub = null;
            if (!string.IsNullOrEmpty(email))
            {
                objSub = _Members.GetMemberSubscriptionByEmail(email, ref status);
            }
            return objSub;
        }
        public bool IsPaymentAlreadyProcessed(string paymentId, ref int status)
        {
            if (string.IsNullOrEmpty(paymentId)) return false;
            return _Members.IsPaymentAlreadyProcessed(paymentId, ref status);
        }

        public void SaveProcessedPayment(string paymentId, long memberId)
        {
            if (!string.IsNullOrEmpty(paymentId))
                _Members.SaveProcessedPayment(paymentId, memberId);
        }
        public Int64 UpdateSubscriptionNextDate(
            Entities.MemberSubscriptions objMemberSubscriptions)
        {
            Int64 _status = 0;
            if (objMemberSubscriptions != null)
            {
                _status = _Members.UpdateSubscriptionNextDate(objMemberSubscriptions);
            }
            return _status;
        }

        // ================================================================
        // 4. UpdateSubscriptionStatus
        // ================================================================
        public Int64 UpdateSubscriptionStatus(
            Entities.MemberSubscriptions objMemberSubscriptions)
        {
            Int64 _status = 0;
            if (objMemberSubscriptions != null)
            {
                _status = _Members.UpdateSubscriptionStatus(objMemberSubscriptions);
            }
            return _status;
        }

        #endregion
    }
}
