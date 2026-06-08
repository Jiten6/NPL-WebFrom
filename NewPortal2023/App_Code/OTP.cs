using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace NewPortal2023.ESS
{
    public class OTP
    {
        private DBUtility objDBUtility;

        public DataSet InsertCandidateDetails()
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Emp_AId == null ? "" : Emp_AId.Trim());
            objDBUtility.AddParameters("@EMPTYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmployeeType == null ? "" : EmployeeType.Trim());

            objDBUtility.AddParameters("@AADHARNO", DBUtilDBType.Varchar, DBUtilDirection.In, 50, AadhaarNo == null ? "" : AadhaarNo.Trim());
            objDBUtility.AddParameters("@STATEPOST", DBUtilDBType.Varchar, DBUtilDirection.In, 50, StateOfPosting == null ? "" : StateOfPosting.Trim());
            objDBUtility.AddParameters("@LOCPOST", DBUtilDBType.Varchar, DBUtilDirection.In, 50, LocationOfPosting == null ? "" : LocationOfPosting.Trim());

            objDBUtility.AddParameters("@DEGINATION", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Degination == null ? "" : Degination.Trim());
            objDBUtility.AddParameters("@DEPARTMENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Department == null ? "" : Department.Trim());

            objDBUtility.AddParameters("@GRADE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Grade == null ? "" : Grade.Trim());
            objDBUtility.AddParameters("@SUCODE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, SUCode == null ? "" : SUCode.Trim());
            objDBUtility.AddParameters("@CANDIDATECAT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CandidateCategory == null ? "" : CandidateCategory.Trim());
            objDBUtility.AddParameters("@MOBILE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Mobile == null ? "" : Mobile.Trim());
            objDBUtility.AddParameters("@AADHARNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 500, nameLabel == null ? "" : nameLabel.Trim());
            objDBUtility.AddParameters("@DOB", DBUtilDBType.Varchar, DBUtilDirection.In, 50, dobLabel == null ? "" : dobLabel.Trim());
            objDBUtility.AddParameters("@GENDER", DBUtilDBType.Varchar, DBUtilDirection.In, 50, genderLabel == null ? "" : genderLabel.Trim());
            objDBUtility.AddParameters("@ADDRESSLABEL", DBUtilDBType.Varchar, DBUtilDirection.In, 50, addressLabel == null ? "" : addressLabel.Trim());
            objDBUtility.AddParameters("@COUNTRY", DBUtilDBType.Varchar, DBUtilDirection.In, 500, country == null ? "" : country.Trim());
            objDBUtility.AddParameters("@STATE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, state == null ? "" : state.Trim());
            objDBUtility.AddParameters("@PINCODE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, pincode == null ? "" : pincode.Trim());
            objDBUtility.AddParameters("@PANNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, panname == null ? "" : panname.Trim());
            objDBUtility.AddParameters("@PANCODE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, PanNo == null ? "" : PanNo.Trim());
            objDBUtility.AddParameters("@ADD1", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Add1 == null ? "" : Add1.Trim());
            objDBUtility.AddParameters("@ADD2", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Add2 == null ? "" : Add2.Trim());
            objDBUtility.AddParameters("@ADD3", DBUtilDBType.Varchar, DBUtilDirection.In, 500, Add3 == null ? "" : Add3.Trim());
            objDBUtility.AddParameters("@EMAILID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, emailId == null ? "" : emailId.Trim());
            objDBUtility.AddParameters("@EXPDOJ", DBUtilDBType.Varchar, DBUtilDirection.In, 50, ExcpDOJ == null ? "" : ExcpDOJ.Trim());
            objDBUtility.AddParameters("@RMK", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Rmk == null ? "" : Rmk.Trim());
            objDBUtility.AddParameters("@PATHAADHAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, PathAadhar == null ? "" : PathAadhar.Trim());
            objDBUtility.AddParameters("@PATHPAN", DBUtilDBType.Varchar, DBUtilDirection.In, 50, PathPan == null ? "" : PathPan.Trim());
            objDBUtility.AddParameters("@PATHEDUC", DBUtilDBType.Varchar, DBUtilDirection.In, 50, PathEduc == null ? "" : PathEduc.Trim());
            objDBUtility.AddParameters("@PATHCC", DBUtilDBType.Varchar, DBUtilDirection.In, 50, PathCC == null ? "" : PathCC.Trim());
            objDBUtility.AddParameters("@LINKSTATUS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, LinkStatus == null ? "" : LinkStatus.Trim()); 

            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "INSERTCANDIDATEDETAILS");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_AADHAARPAN");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        

        public DataSet UpdateCandidateDetails()
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EntryAId == null ? "" : EntryAId.Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Emp_AId == null ? "" : Emp_AId.Trim());
            objDBUtility.AddParameters("@EMPTYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmployeeType == null ? "" : EmployeeType.Trim());
            objDBUtility.AddParameters("@AADHARNO", DBUtilDBType.Varchar, DBUtilDirection.In, 50, AadhaarNo == null ? "" : AadhaarNo.Trim());
            objDBUtility.AddParameters("@STATEPOST", DBUtilDBType.Varchar, DBUtilDirection.In, 50, StateOfPosting == null ? "" : StateOfPosting.Trim());
            objDBUtility.AddParameters("@LOCPOST", DBUtilDBType.Varchar, DBUtilDirection.In, 50, LocationOfPosting == null ? "" : LocationOfPosting.Trim());
            objDBUtility.AddParameters("@GRADE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Grade == null ? "" : Grade.Trim());
            objDBUtility.AddParameters("@SUCODE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, SUCode == null ? "" : SUCode.Trim());
            objDBUtility.AddParameters("@CANDIDATECAT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CandidateCategory == null ? "" : CandidateCategory.Trim());
            objDBUtility.AddParameters("@MOBILE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Mobile == null ? "" : Mobile.Trim());
            objDBUtility.AddParameters("@AADHARNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 500, nameLabel == null ? "" : nameLabel.Trim());
            objDBUtility.AddParameters("@DOB", DBUtilDBType.Varchar, DBUtilDirection.In, 50, dobLabel == null ? "" : dobLabel.Trim());
            objDBUtility.AddParameters("@GENDER", DBUtilDBType.Varchar, DBUtilDirection.In, 50, genderLabel == null ? "" : genderLabel.Trim());
            objDBUtility.AddParameters("@ADDRESSLABEL", DBUtilDBType.Varchar, DBUtilDirection.In, 50, addressLabel == null ? "" : addressLabel.Trim());
            objDBUtility.AddParameters("@COUNTRY", DBUtilDBType.Varchar, DBUtilDirection.In, 500, country == null ? "" : country.Trim());
            objDBUtility.AddParameters("@STATE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, state == null ? "" : state.Trim());
            objDBUtility.AddParameters("@PINCODE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, pincode == null ? "" : pincode.Trim());
            objDBUtility.AddParameters("@PANNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, panname == null ? "" : panname.Trim());
            objDBUtility.AddParameters("@PANCODE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, PanNo == null ? "" : PanNo.Trim());
            objDBUtility.AddParameters("@ADD1", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Add1 == null ? "" : Add1.Trim());
            objDBUtility.AddParameters("@ADD2", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Add2 == null ? "" : Add2.Trim());
            objDBUtility.AddParameters("@ADD3", DBUtilDBType.Varchar, DBUtilDirection.In, 500, Add3 == null ? "" : Add3.Trim());
            objDBUtility.AddParameters("@EMAILID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, emailId == null ? "" : emailId.Trim());
            objDBUtility.AddParameters("@EXPDOJ", DBUtilDBType.Varchar, DBUtilDirection.In, 50, ExcpDOJ == null ? "" : ExcpDOJ.Trim());
            objDBUtility.AddParameters("@RMK", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Rmk == null ? "" : Rmk.Trim());
            objDBUtility.AddParameters("@PATHAADHAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, PathAadhar == null ? "" : PathAadhar.Trim());
            objDBUtility.AddParameters("@PATHPAN", DBUtilDBType.Varchar, DBUtilDirection.In, 50, PathPan == null ? "" : PathPan.Trim());
            objDBUtility.AddParameters("@PATHEDUC", DBUtilDBType.Varchar, DBUtilDirection.In, 50, PathEduc == null ? "" : PathEduc.Trim());
            objDBUtility.AddParameters("@PATHCC", DBUtilDBType.Varchar, DBUtilDirection.In, 50, PathCC == null ? "" : PathCC.Trim());

            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATECANDIDATEDETAILS");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_AADHAARPAN");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }




        public DataSet fillPanData(string lblpan, string panname)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@PANNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, panname == null ? "" : panname.Trim());
            //objDBUtility.AddParameters("@TODATE", DBUtilDBType.Varchar, DBUtilDirection.In, 500, genderLabel == null ? "" : genderLabel.Trim());
            objDBUtility.AddParameters("@PANNO", DBUtilDBType.Varchar, DBUtilDirection.In, 50, lblpan == null ? "" : lblpan.Trim());
            //objDBUtility.AddParameters("@TYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, addressLabel == null ? "" : addressLabel.Trim());
            //objDBUtility.AddParameters("@country", DBUtilDBType.Varchar, DBUtilDirection.In, 500, country == null ? "" : country.Trim());
            //objDBUtility.AddParameters("@state", DBUtilDBType.Varchar, DBUtilDirection.In, 50, state == null ? "" : state.Trim());
            //objDBUtility.AddParameters("@pincode", DBUtilDBType.Varchar, DBUtilDirection.In, 50, pincode == null ? "" : pincode.Trim());

            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "FILLPANDATA");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }
        public DataSet tokengenerate()
        {
            objDBUtility = new DBUtility();

            DataSet ds = null;
            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "TOKENGENERATE");
            ds = objDBUtility.Execute_StoreProc_DataSet("SP_AADHAARPAN");
            return ds;
        }
        public DataSet tokenINSERT(string token)
        {
            objDBUtility = new DBUtility();

            DataSet ds = null;
            objDBUtility.AddParameters("@TOKEN", DBUtilDBType.Varchar, DBUtilDirection.In, 50, token == null ? "" : token.Trim());
            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "TOKENINSERT");
            ds = objDBUtility.Execute_StoreProc_DataSet("SP_AADHAARPAN");
            return ds;
        }




        public void FillCandidates(GridView vwList, string pageIndex, string pageSize)
        {
            objDBUtility = new DBUtility();

            DataSet ds = null;
            objDBUtility.AddParameters("@PAGEINDEX", DBUtilDBType.Varchar, DBUtilDirection.In, 50, pageIndex.ToString().Trim());
            objDBUtility.AddParameters("@PAGESIZE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, pageSize.ToString().Trim());
            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "FILLCANDIDATES");
            ds = objDBUtility.Execute_StoreProc_DataSet("SP_AADHAARPAN");
            vwList.DataSource = ds;
            vwList.DataBind();

            if (vwList.HeaderRow != null)
            {
                vwList.HeaderRow.TableSection = TableRowSection.TableHeader;
            }

            objDBUtility.ClearTransactionalParams();
        }


        public DataSet FillCandidates()
        {
            objDBUtility = new DBUtility();

            DataSet ds = null;
            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "FILLCANDIDATES");
            ds = objDBUtility.Execute_StoreProc_DataSet("SP_AADHAARPAN");
            return ds;
        }

        public DataSet Fill_CandidateDetailByID()
        {
            objDBUtility = new DBUtility();

            DataSet ds = null;
            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EntryAId == null ? "" : EntryAId.Trim());
            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "FILLCANDIDATEBYID");
            ds = objDBUtility.Execute_StoreProc_DataSet("SP_AADHAARPAN");
            return ds;
        }

        public DataSet FillDetailsByAadhar()
        {
            objDBUtility = new DBUtility();

            DataSet ds = null;
            objDBUtility.AddParameters("@AADHARNO", DBUtilDBType.Varchar, DBUtilDirection.In, 50, AadhaarNo == null ? "" : AadhaarNo.Trim());
            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "FILLCANDIDATEBYAADHAR");
            ds = objDBUtility.Execute_StoreProc_DataSet("SP_AADHAARPAN");
            return ds;
        }


        public DataSet UpdateCandidateStatus()
        {
            objDBUtility = new DBUtility();

            DataSet ds = null;

            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Emp_AId == null ? "" : Emp_AId.Trim());
            objDBUtility.AddParameters("@CASENO", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CaseNo == null ? "" : CaseNo.Trim());
            objDBUtility.AddParameters("@RMK", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Rmk == null ? "" : Rmk.Trim());
            objDBUtility.AddParameters("@STATUS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Status == null ? "" : Status.Trim());
            objDBUtility.AddParameters("@ACTION_TYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, ActionType == null ? "" : ActionType.Trim());


            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATECANDIDATESTATUS");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_AADHAARPAN");
            objDBUtility.ClearTransactionalParams();
            return ds;
        }


        public DataSet UpdateCandidateHiringStatus()
        {
            objDBUtility = new DBUtility();

            DataSet ds = null;

            
            objDBUtility.AddParameters("@CASENO", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CaseNo == null ? "" : CaseNo.Trim());
            objDBUtility.AddParameters("@RMK", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Rmk == null ? "" : Rmk.Trim());
            objDBUtility.AddParameters("@STATUS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Status == null ? "" : Status.Trim());
            objDBUtility.AddParameters("@ACTION_TYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, ActionType == null ? "" : ActionType.Trim());


            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATEHIRINGSTATUS");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_AADHAARPAN");
            objDBUtility.ClearTransactionalParams();
            return ds;
        }



        public DataSet Candidate_Login(string code, string password)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "CADIDATELOGIN");
            // objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@PANCODE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, code.ToString().Trim());
            objDBUtility.AddParameters("@MOBILE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, password.ToString().Trim());

            dsLogin = objDBUtility.Execute_StoreProc_DataSet("SP_AADHAARPAN");
            objDBUtility.ClearTransactionalParams();
            return dsLogin;
        }

        public DataSet DownloadOfferLetter()
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@EMAILID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, emailId.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "DOWNLOADOFFERLETTER");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_AADHAARPAN");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        public DataSet Fill_CandidateDetailByPanID()
        {
            objDBUtility = new DBUtility();

            DataSet ds = null;
            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EntryAId == null ? "" : EntryAId.Trim());
            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "FILLCANDIDATEBYPANID");
            ds = objDBUtility.Execute_StoreProc_DataSet("SP_AADHAARPAN");
            return ds;
        }

        public DataSet GetMailID()
        {
            objDBUtility = new DBUtility();

            DataSet ds = null;
            objDBUtility.AddParameters("@PANCODE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, PanNo == null ? "" : PanNo.Trim());
            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETMAILID");
            ds = objDBUtility.Execute_StoreProc_DataSet("SP_AADHAARPAN");
            return ds;
        }

        public DataSet InsertOTP()
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@EMAILID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, emailId.ToString().Trim());
            objDBUtility.AddParameters("@OTP", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Option.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "INSERTOTP");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_AADHAARPAN");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        public DataSet CheckOTP()
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@EMAILID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, emailId.ToString().Trim());
            objDBUtility.AddParameters("@OTP", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Option.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "CHECKOTP");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_AADHAARPAN");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }
        public DataSet AcceptCandidateDetailByAadharID()
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@AADHARNO", DBUtilDBType.Varchar, DBUtilDirection.In, 50, AadhaarNo == null ? "" : AadhaarNo.Trim());
            objDBUtility.AddParameters("@RMK", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Rmk == null ? "" : Rmk.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "ACCEPTCANDIDATE");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_AADHAARPAN");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }
        public DataSet RejectCandidateDetailByAadharID()
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@AADHARNO", DBUtilDBType.Varchar, DBUtilDirection.In, 50, AadhaarNo == null ? "" : AadhaarNo.Trim());
            objDBUtility.AddParameters("@RMK", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Rmk == null ? "" : Rmk.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "REJECTCANDIDATE");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_AADHAARPAN");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        public DataSet FillCandidatesHiring()
        {
            objDBUtility = new DBUtility();

            DataSet ds = null;
            objDBUtility.AddParameters("@EMPTYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmployeeType == null ? "" : EmployeeType.Trim());
            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "FILLCANDIDATESHIRING");
            ds = objDBUtility.Execute_StoreProc_DataSet("SP_AADHAARPAN");
            return ds;
        }


        internal DataSet CheckAadharEntry()
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@AADHARNO", DBUtilDBType.Varchar, DBUtilDirection.In, 50, AadhaarNo == null ? "" : AadhaarNo.Trim());

            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "CHECKAADHARENTRY");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_AADHAARPAN");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        public DataSet FillCandidatesHR()
        {
            objDBUtility = new DBUtility();

            DataSet ds = null;
            objDBUtility.AddParameters("@EMPTYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmployeeType == null ? "" : EmployeeType.Trim());
            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "FILLCANDIDATEHR");
            ds = objDBUtility.Execute_StoreProc_DataSet("SP_AADHAARPAN");
            return ds;
        }


        string _PanNo;
        string _EmployeeType;
        string _AadhaarNo;
        string _StateOfPosting;
        string _LocationOfPosting;
        string _Department;
        string _Grade;
        string _Degination;
        string _SUCode;
        string _CandidateCategory;
        string _Mobile;
        string _nameLabel;
        string _genderLabel;
        string _dobLabel;
        string _addressLabel;
        string _country;
        string _state;
        string _pincode;
        string _panname;
        string _EntryAId;
        string _Add1;
        string _Add2;
        string _Add3;
        string _emailId;
        string _ExcpDOJ;
        string _Rmk;
        string _PathAadhar;
        string _PathPan;
        string _PathEduc;
        string _PathCC;
        string _Emp_AId;
        string _Option;
        string _ActionType;
        string _Status;
        string _CaseNo;
        string _LinkStatus;

        public string ActionType
        {
            get { return _ActionType; }
            set { _ActionType = value; }
        }
        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        public string CaseNo
        {
            get { return _CaseNo; }
            set { _CaseNo = value; }
        }

        public string Option
        {
            get { return _Option; }
            set { _Option = value; }
        }

        public string Emp_AId
        {
            get { return _Emp_AId; }
            set { _Emp_AId = value; }
        }

        public string PathAadhar
        {
            get { return _PathAadhar; }
            set { _PathAadhar = value; }
        }
        public string PathPan
        {
            get { return _PathPan; }
            set { _PathPan = value; }
        }
        public string PathEduc
        {
            get { return _PathEduc; }
            set { _PathEduc = value; }
        }
        public string PathCC
        {
            get { return _PathCC; }
            set { _PathCC = value; }
        }
        public string Add1
        {
            get { return _Add1; }
            set { _Add1 = value; }
        }


        public string Add2
        {
            get { return _Add2; }
            set { _Add2 = value; }
        }


        public string Add3
        {
            get { return _Add3; }
            set { _Add3 = value; }
        }

        public string emailId
        {
            get { return _emailId; }
            set { _emailId = value; }
        }

        public string ExcpDOJ
        {
            get { return _ExcpDOJ; }
            set { _ExcpDOJ = value; }
        }


        public string Rmk
        {
            get { return _Rmk; }
            set { _Rmk = value; }
        }




        public string EntryAId
        {
            get { return _EntryAId; }
            set { _EntryAId = value; }
        }


        public string nameLabel
        {
            get { return _nameLabel; }
            set { _nameLabel = value; }
        }

        public string genderLabel
        {
            get { return _genderLabel; }
            set { _genderLabel = value; }
        }

        public string dobLabel
        {
            get { return _dobLabel; }
            set { _dobLabel = value; }
        }

        public string addressLabel
        {
            get { return _addressLabel; }
            set { _addressLabel = value; }
        }

        public string country
        {
            get { return _country; }
            set { _country = value; }
        }

        public string state
        {
            get { return _state; }
            set { _state = value; }
        }
        public string pincode
        {
            get { return _pincode; }
            set { _pincode = value; }
        }

        public string panname
        {
            get { return _panname; }
            set { _panname = value; }
        }

        public string LocationOfPosting
        {
            get { return _LocationOfPosting; }
            set { _LocationOfPosting = value; }
        }

        public string Mobile
        {
            get { return _Mobile; }
            set { _Mobile = value; }
        }

        public string StateOfPosting
        {
            get { return _StateOfPosting; }
            set { _StateOfPosting = value; }
        }

        public string Department
        {
            get { return _Department; }
            set { _Department = value; }
        }
        public string Grade
        {
            get { return _Grade; }
            set { _Grade = value; }
        }

        public string Degination
        {
            get { return _Degination; }
            set { _Degination = value; }
        }
        public string SUCode
        {
            get { return _SUCode; }
            set { _SUCode = value; }
        }
        public string CandidateCategory
        {
            get { return _CandidateCategory; }
            set { _CandidateCategory = value; }
        }
        public string EmployeeType
        {
            get { return _EmployeeType; }
            set { _EmployeeType = value; }
        }

        public string AadhaarNo
        {
            get { return _AadhaarNo; }
            set { _AadhaarNo = value; }
        }
        public string PanNo
        {
            get { return _PanNo; }
            set { _PanNo = value; }
        }
        public string LinkStatus
        {
            get { return _LinkStatus; }
            set { _LinkStatus = value; }
        }

    }
}