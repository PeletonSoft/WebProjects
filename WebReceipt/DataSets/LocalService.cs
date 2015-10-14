using System.Data.SqlClient;
using System.Linq;
using DBConverting;
using System.Data;
using System;
using System.Configuration;
using WebReceipt.DataSets;

namespace LocalService
{
    class LocalNumService
    {
        static double StrToDouble(string s)
        {
            //System.Globalization.CultureInfo.GetCultureInfo()
            return Convert.ToDouble(s.Replace('.', ','));
        }
    }
    class LocalDBService
    {
        static public string GetConnectionString()
        {
            return ConfigurationManager.
                ConnectionStrings["DataConnectionString"].ConnectionString;
        }

        static private SqlConnection Database()
        {
            return new SqlConnection(GetConnectionString());
            
        }


        public static void ReceiptEdit(
            ref int? PositionId,
            ref int? StoreInfoId,
            out int ArticleId,
            ref double? Quant,
            ref double? Discount,
            out double Price,
            int? ShopId,
            int? UserId,
            ref int? ReceiptNumber,
            int Edit) // тип редактирования (-1 - DELETE, 0 - UPDATE, +1 - INSERT)
        {

            SqlConnection db = Database();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = db;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[КПК_чеки_веб_редактирование]";

            cmd.Parameters.Add("@PositionId", SqlDbType.Int).Direction = ParameterDirection.InputOutput;
            cmd.Parameters.Add("@StoreInfoId", SqlDbType.Int).Direction = ParameterDirection.InputOutput;
            cmd.Parameters.Add("@ArticleId", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@Quant", SqlDbType.Float).Direction = ParameterDirection.InputOutput;
            cmd.Parameters.Add("@Discount", SqlDbType.Float).Direction = ParameterDirection.InputOutput;
            cmd.Parameters.Add("@Price", SqlDbType.Float).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@ShopId", SqlDbType.Int).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("@UserId", SqlDbType.Int).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("@ReceiptNumber", SqlDbType.Int).Direction = ParameterDirection.InputOutput;
            cmd.Parameters.Add("@Edit", SqlDbType.Int).Direction = ParameterDirection.Input;

            cmd.Parameters["@PositionId"].Value = DBConvert.ToDBObject(PositionId);
            cmd.Parameters["@StoreInfoId"].Value = DBConvert.ToDBObject(StoreInfoId);
            cmd.Parameters["@Quant"].Value = DBConvert.ToDBObject(Quant);
            cmd.Parameters["@Discount"].Value = DBConvert.ToDBObject(Discount);
            cmd.Parameters["@ShopId"].Value = DBConvert.ToDBObject(ShopId);
            cmd.Parameters["@UserId"].Value = DBConvert.ToDBObject(UserId);
            cmd.Parameters["@ReceiptNumber"].Value = DBConvert.ToDBObject(ReceiptNumber);
            cmd.Parameters["@Edit"].Value = DBConvert.ToDBObject(Edit);

            db.Open();
            try
            {
                cmd.ExecuteNonQuery();
            }
            finally
            {
                db.Close();
            }

            PositionId = DBConvert.ToQInt(cmd.Parameters["@PositionId"].Value);
            StoreInfoId = DBConvert.ToQInt(cmd.Parameters["@StoreInfoId"].Value);
            ArticleId = DBConvert.ToInt(cmd.Parameters["@ArticleId"].Value);
            Quant = DBConvert.ToQDouble(cmd.Parameters["@Quant"].Value);
            Discount = DBConvert.ToQDouble(cmd.Parameters["@Discount"].Value);
            Price = DBConvert.ToDouble(cmd.Parameters["@Price"].Value);
            ReceiptNumber = DBConvert.ToInt(cmd.Parameters["@ReceiptNumber"].Value);
        }

        public static void ReceiptInsert(
            out int PositionId,
            int StoreInfoId,
            out int ArticleId,
            out double Quant,
            out double Discount,
            out double Price,
            int ShopId,
            int UserId,
            ref int? ReceiptNumber)
        {
            int? _PositionId = null;
            int? _StoreInfoId = StoreInfoId;
            double? _Quant = null;
            double? _Discount = null;

            ReceiptEdit(
                ref _PositionId, ref _StoreInfoId, out ArticleId, ref _Quant,
                ref _Discount, out Price, ShopId, UserId, ref ReceiptNumber, 1);

            PositionId = (int)_PositionId;
            Quant = (double)_Quant;
            Discount = (double)_Discount;
        }

        public static void ReceiptInsert(
            int StoreInfoId,
            int ShopId,
            int UserId,
            ref int? ReceiptNumber,
            out int PositionId)
        {
            int _ArticleId;
            double _Quant;
            double _Discount;
            double _Price;

            ReceiptInsert(
                out PositionId, StoreInfoId, out _ArticleId, out _Quant,
                out _Discount, out _Price, ShopId, UserId, ref ReceiptNumber);
        }
        public static void ReceiptUpdate(
            int PositionId,
            ref double Quant,
            ref double Discount,
            out double Price)
        {
            int? _PositionId = PositionId;
            int? _StoreInfoId = null;
            double? _Quant = Quant;
            double? _Discount = Discount;
            int _ArticleId;
            int? _ReceiptNumber = null;

            ReceiptEdit(
                ref _PositionId, ref _StoreInfoId, out _ArticleId, ref _Quant,
                ref _Discount, out Price, null, null, ref _ReceiptNumber, 0);

            Quant = (double)_Quant;
            Discount = (double)_Discount;
        }


        public static void ReceiptUpdate(
            int PositionId,
            double Quant,
            double Discount)
        {
            double _Price;
            ReceiptUpdate(PositionId, ref Quant, ref Discount, out _Price);
        }

        public static int ReceiptDelete(
            int PositionId)
        {
            int? _PositionId = PositionId;
            int? _StoreInfoId = null;
            double? _Quant = null;
            double? _Discount = null;
            int _ArticleId;
            int? _ReceiptNumber = null;
            double Price;
            ReceiptEdit(
                ref _PositionId, ref _StoreInfoId, out _ArticleId, ref _Quant,
                ref _Discount, out Price, null, null, ref _ReceiptNumber, -1);
            if (_ReceiptNumber == null || _ReceiptNumber <= 0)
                return 0;
            return (int)_ReceiptNumber;
        }

        public static bool CheckPassword(
            int UserId,
            string Password)
        {
            SqlConnection db = Database();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = db;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT [Код] FROM [Сотрудники] WHERE [Код]=@UserId AND ISNULL([Password],'')=@Password";

            cmd.Parameters.Add("@UserId", SqlDbType.Int).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("@Password", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Input;

            cmd.Parameters["@UserId"].Value = UserId;
            cmd.Parameters["@Password"].Value = Password;

            cmd.Connection = db;

            object res = null;
            db.Open();
            try
            {
                res = cmd.ExecuteScalar();
            }
            finally
            {
                db.Close();
            }

            return (res != null && res != DBNull.Value);
        }

        public static void InfoByArticleId(
            int ShopId,
            int ArticleId,
            out int ErrorId,
            out bool Error,
            out bool Warning,
            out string Message,
            out int PosCount,
            out int StoreInfoId)
        {
            SqlConnection db = Database();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = db;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[КПК_чеки_справка_по_артикулу]";

            cmd.Parameters.Add("@ShopId", SqlDbType.Int).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("@ArticleId", SqlDbType.Int).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("@PositionId", SqlDbType.Int).Direction = ParameterDirection.Input;

            cmd.Parameters.Add("@StoreInfoId", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@PosCount", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@CodeError", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@Error", SqlDbType.Bit).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@Warning", SqlDbType.Bit).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@Messgage", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;

            cmd.Parameters["@ArticleId"].Value = ArticleId;
            cmd.Parameters["@ShopId"].Value = ShopId;
            cmd.Parameters["@PositionId"].Value = 0;

            db.Open();
            try
            {
                cmd.ExecuteNonQuery();
            }
            finally
            {
                db.Close();
            }

            ErrorId = Convert.ToInt32(cmd.Parameters["@CodeError"].Value);
            Error = Convert.ToBoolean(cmd.Parameters["@Error"].Value);
            Warning = Convert.ToBoolean(cmd.Parameters["@Warning"].Value);
            PosCount = Convert.ToInt32(cmd.Parameters["@PosCount"].Value);
            StoreInfoId = Convert.ToInt32(cmd.Parameters["@StoreInfoId"].Value);
            Message = cmd.Parameters["@Messgage"].Value.ToString();
        }

        public static int FirstReceiptNumber(
            int UserId,
            int ShopId)
        {
            SqlConnection db = Database();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = db;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT [dbo].[КПК_чек_первый_открытый](@ShopId,@UserId)";

            cmd.Parameters.Add("@UserId", SqlDbType.Int).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("@ShopId", SqlDbType.Int).Direction = ParameterDirection.Input;

            cmd.Parameters["@UserId"].Value = UserId;
            cmd.Parameters["@ShopId"].Value = ShopId;

            cmd.Connection = db;

            object res = null;
            db.Open();
            try
            {
                res = cmd.ExecuteScalar();
            }
            finally
            {
                db.Close();
            }

            if (res == null || res == DBNull.Value)
                return 0;
            return Convert.ToInt32(res);
        }

        public static void ReceiptClear(int ShopId, int ReceiptNumber)
        {
            SqlConnection db = Database();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = db;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[КПК_чеки_удаление_чека]";

            cmd.Parameters.Add("@Shop", SqlDbType.Int).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("@Check", SqlDbType.Int).Direction = ParameterDirection.Input;

            cmd.Parameters["@Shop"].Value = ShopId;
            cmd.Parameters["@Check"].Value = ReceiptNumber;


            db.Open();
            try
            {
                cmd.ExecuteNonQuery();
            }
            finally
            {
                db.Close();
            }

        }
        public static bool ReceiptSend(
            int ShopId, int ReceiptNumber, 
            string UserName, out string Message)
        {
            SqlConnection db = Database();
            var cmd = new SqlCommand();
            cmd.Connection = db;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "КПК_чеки_отправка_чека";

            cmd.Parameters.Add("@Shop", SqlDbType.Int).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("@Check", SqlDbType.Int).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("@From", SqlDbType.VarChar, 100).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("@To", SqlDbType.Int).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("@Error", SqlDbType.Bit).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@CodeError", SqlDbType.Int).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@Msg", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;

            cmd.Parameters["@Shop"].Value = ShopId;
            cmd.Parameters["@Check"].Value = ReceiptNumber;
            cmd.Parameters["@From"].Value = UserName;
            cmd.Parameters["@To"].Value = 0;

            db.Open();
            try
            {
                cmd.ExecuteNonQuery();
            }
            finally
            {
                db.Close();
            }
            //int ErrorId = Convert.ToInt32(cmd.Parameters["@CodeError"].Value);
            Message = cmd.Parameters["@Msg"].Value.ToString();

            return Convert.ToBoolean(cmd.Parameters["@Error"].Value);

        }

        internal static bool Revise(int ShopId, int ArticleId, int Price)
        {
            var connectionString = ConfigurationManager.
                ConnectionStrings["DataConnectionString"].ConnectionString;
            var dc = new ReceiptClassDataContext(connectionString);
            var last = dc.Ассортимент_Ревизияs
                .Where(r => r.Код_магазина == ShopId)
                .OrderByDescending(r => r.Дата_начала)
                .First();

            if (last == null || !(last.Статус == 0 || last.Статус == 1))
                return true;

            dc.Ассортимент_зал_добавить(ArticleId, Price, last.Код);
            return false;
        }
    }
}
