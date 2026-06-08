using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace NewPortal2023.ESS
{
    public class PmsHr
    {
        private NewPortal2023.ESS.DBUtility objDBUtility;
        private NewPortal2023.ESS.Common objCommon;

        public DataSet UpdatedKRAFlag()
        {
            objDBUtility = new DBUtility();
            DataSet ds = null;

            //objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue == null ? "" : empValue.Trim());
            //objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue == null ? "" : compValue.Trim());
            //objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
            //objDBUtility.AddParameters("@Quarter", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Quarter == null ? "" : Quarter.Trim());
            objDBUtility.AddParameters("@TYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Type == null ? "" : Type.Trim());
            objDBUtility.AddParameters("@KRAFLAG", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Flag == null ? "" : Flag.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATEDKRAFLAGALL");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_PMSHR");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }


        public DataSet UpdatedKRAFlagAll()
        {
            objDBUtility = new DBUtility();
            DataSet ds = null;

            //objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue == null ? "" : empValue.Trim());
            //objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue == null ? "" : compValue.Trim());
            //objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
            //objDBUtility.AddParameters("@Quarter", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Quarter == null ? "" : Quarter.Trim());
            objDBUtility.AddParameters("@TYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Type == null ? "" : Type.Trim());
            objDBUtility.AddParameters("@KRAFLAG", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Flag == null ? "" : Flag.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATEDKRAFLAGALL");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_PMSHR");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }
        public DataSet InsertDateKRAFlag()
        {
            objDBUtility = new DBUtility();
            DataSet ds = null;

            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode == null ? "" : EmpCode.Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Comp_Aid == null ? "" : Comp_Aid.Trim());
            objDBUtility.AddParameters("@DATE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, ToDate == null ? "" : ToDate.Trim());
            //objDBUtility.AddParameters("@Quarter", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Quarter == null ? "" : Quarter.Trim());
            objDBUtility.AddParameters("@TYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Type == null ? "" : Type.Trim());
           // objDBUtility.AddParameters("@KRAFLAG", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Flag == null ? "" : Flag.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "INSERTDATEKRAFLAG");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_PMSHR");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        public DataSet UpdateDateKRAFlag()
        {
            objDBUtility = new DBUtility();
            DataSet ds = null;

            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode == null ? "" : EmpCode.Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Comp_Aid == null ? "" : Comp_Aid.Trim());
            objDBUtility.AddParameters("@DATE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, ToDate == null ? "" : ToDate.Trim());
            //objDBUtility.AddParameters("@Quarter", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Quarter == null ? "" : Quarter.Trim());
            objDBUtility.AddParameters("@TYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Type == null ? "" : Type.Trim());
            // objDBUtility.AddParameters("@KRAFLAG", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Flag == null ? "" : Flag.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATEDATEKRAFLAG");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_PMSHR");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }
    

        public DataSet GetList() 
        {
            objDBUtility = new DBUtility();
            DataSet ds = null;

            //objDBUtility.AddParameters("@KRA_ACTIVATION_FLAG", DBUtilDBType.Varchar, DBUtilDirection.In, 50, KRA_Activation_Flag == null ? "" : KRA_Activation_Flag.Trim());
            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETKRAFLAG");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_PMSHR");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }


        internal DataSet GetPmsActivationDates()
        {
            objDBUtility = new DBUtility();
            DataSet ds = null;

            //objDBUtility.AddParameters("@KRA_ACTIVATION_FLAG", DBUtilDBType.Varchar, DBUtilDirection.In, 50, KRA_Activation_Flag == null ? "" : KRA_Activation_Flag.Trim());
            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETPMSACTDATES");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_PMSHR");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        internal void CheckPMSdates()
        {
            objDBUtility = new DBUtility();
            DataSet ds = null;

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATEPMSACTDATESAUTO");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_PMSHR");

            objDBUtility.ClearTransactionalParams();
        }

        internal DataSet UpdatePMSActivationDates()
        {
            objDBUtility = new DBUtility();
            DataSet ds = null;

            objDBUtility.AddParameters("@FROMDATEEMP", DBUtilDBType.Varchar, DBUtilDirection.In, 50, FromDateEmp == null ? "" : FromDateEmp.Trim());
            objDBUtility.AddParameters("@TODATEEMP", DBUtilDBType.Varchar, DBUtilDirection.In, 50, ToDateEmp == null ? "" : ToDateEmp.Trim());
            objDBUtility.AddParameters("@FROMDATEAPPR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, FromDateAppr == null ? "" : FromDateAppr.Trim());
            objDBUtility.AddParameters("@TODATEAPPR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, ToDateAppr == null ? "" : ToDateAppr.Trim());
            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATEPMSACTDATES");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_PMSHR");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        internal DataSet GetPMSEmpFlag(string Comp_Aid, string Emp_Aid)
        {
            objDBUtility = new DBUtility();
            DataSet ds = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Comp_Aid == null ? "" : Comp_Aid.Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Emp_Aid == null ? "" : Emp_Aid.Trim());
            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETPMSEMPFLAG");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_PMSHR");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        public DataSet GetActivationDate()
        {
            objDBUtility = new DBUtility();
            DataSet ds = null;
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETKRAACTIVATIONVDATE");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_PMSHR");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        

        public DataSet GetKRARejectCount()
        {
            objDBUtility = new DBUtility();
            DataSet ds = null;
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETKRAREJECTEDLIST");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_PMSHR");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }


        public DataSet UpdateKRAFlagOneEmp()
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            //objDBUtility.AddParameters("@Xml", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, xmlValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 100, EmpCode.ToString().Trim());
            objDBUtility.AddParameters("@KRAFLAGSTATUS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, KRA_Flag_Status == null ? "" : KRA_Flag_Status.Trim());
            objDBUtility.AddParameters("@KRAFLAG", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Flag == null ? "" : Flag.Trim());
            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
            //objDBUtility.AddParameters("@ACTIONTYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, drpActionAll.ToString().Trim());

            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATEDKRAFLAGONEEMP");
            dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_PMSHR");

            objDBUtility.ClearTransactionalParams();

            return dsInv;
        }
        public DataSet GetKraStatus()
        {
            objDBUtility = new DBUtility();
            DataSet ds = null;
            objDBUtility.AddParameters("@TYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Type == null ? "" : Type.Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Comp_Aid == null ? "" : Comp_Aid.Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode == null ? "" : EmpCode.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETKRASTATUSDETAILSONE");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_PMSHR");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        internal DataSet GetKRAFlagStatus()
        {
            objDBUtility = new DBUtility();
            DataSet ds = null;

            //objDBUtility.AddParameters("@KRA_ACTIVATION_FLAG", DBUtilDBType.Varchar, DBUtilDirection.In, 50, KRA_Activation_Flag == null ? "" : EmpCode.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETKRAFLAGSTATUS");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_PMSHR");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        internal DataSet GetEmpKRAFlagStatus()
        {
            objDBUtility = new DBUtility();
            DataSet ds = null;

            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode == null ? "" : EmpCode.Trim());
            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETEMPKRAFLAGSTATUS");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_PMSHR");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        public DataSet GetKraStatusone(string Comp_Aid, string EmpCode)
        {
            objDBUtility = new DBUtility();
            DataSet ds = null;
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Comp_Aid == null ? "" : Comp_Aid.Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode == null ? "" : EmpCode.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETKRASTATUSDETAILS");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_PMSHR");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }



        string _Flag;
        
        string _Type;
        string _ToDate;
        string _EmpCode;
        string _Comp_Aid;
        string _ColumnType;
        string _KRA_Activation_Flag;
        string _KRA_Flag_Status;
        string _Year;
        string _FromDateEmp;
        string _ToDateEmp;
        string _FromDateAppr;
        string _ToDateAppr;

        public string ToDateAppr
        {
            get { return _ToDateAppr; }
            set { _ToDateAppr = value; }
        }

        public string FromDateAppr
        {
            get { return _FromDateAppr; }
            set { _FromDateAppr = value; }
        }

        public string ToDateEmp
        {
            get { return _ToDateEmp; }
            set { _ToDateEmp = value; }
        }

        public string FromDateEmp
        {
            get { return _FromDateEmp; }
            set { _FromDateEmp = value; }
        }

        public string Year
        {
            get { return _Year; }
            set { _Year = value; }
        }
        public string Comp_Aid
        {
            get { return _Comp_Aid; }
            set { _Comp_Aid = value; }
        }
        public string EmpCode
        {
            get { return _EmpCode; }
            set { _EmpCode = value; }
        }

        public string Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        public string ToDate
        {
            get { return _ToDate; }
            set { _ToDate = value; }
        }
     
        public string Flag
        {
            get { return _Flag; }
            set { _Flag = value; }
        }
        public string KRA_Activation_Flag
        {
            get { return _KRA_Activation_Flag; }
            set { _KRA_Activation_Flag = value; }
        }
        public string KRA_Flag_Status
        {
            get { return _KRA_Flag_Status; }
            set { _KRA_Flag_Status = value; }
        }



    }
}