using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace login
{
    public class SqlDB
    {
        string strConn = "server=DESKTOP-F4OAI0S;database=mpuser;uid=sa;pwd=12345;";
        SqlConnection Conn;
        SqlDataAdapter dataAdapter;
        DataSet dataSet;

        public SqlDB()
        {
            Conn = new SqlConnection(strConn);
            dataAdapter = new SqlDataAdapter("SELECT * FROM UserInfo", Conn);
            dataSet = new DataSet();

            SqlCommandInit();
            // dataSet 초기화 추가
            dataAdapter.Fill(dataSet, "UserInfo");
        }

        private void SqlCommandInit()
        {
            string strSelectSQL = "SELECT * FROM UserInfo";
            dataAdapter.SelectCommand = new SqlCommand(strSelectSQL, Conn);

            // InsertCommand 설정
            dataAdapter.InsertCommand = new SqlCommand("INSERT INTO UserInfo (uid, pwd, name, score) VALUES (@uid, @pwd, @name, @score)", Conn);
            dataAdapter.InsertCommand.Parameters.Add("@uid", SqlDbType.NVarChar, 15, "uid");
            dataAdapter.InsertCommand.Parameters.Add("@pwd", SqlDbType.NVarChar, 15, "pwd");
            dataAdapter.InsertCommand.Parameters.Add("@name", SqlDbType.NVarChar, 15, "name");
            dataAdapter.InsertCommand.Parameters.Add("@score", SqlDbType.NVarChar, 15, "score");

            ////// DeleteCommand 설정
            ////dataAdapter.DeleteCommand = new SqlCommand("DELETE FROM UserInfo WHERE uid = @uid", Conn);
            ////dataAdapter.DeleteCommand.Parameters.Add("@uid", SqlDbType.NVarChar, 15, "uid");

            //// UpdateCommand 설정
            dataAdapter.UpdateCommand = new SqlCommand("UPDATE UserInfo SET score=@score WHERE uid =@uid", Conn);
            dataAdapter.UpdateCommand.Parameters.Add("@uid", SqlDbType.NVarChar, 15, "uid");
            dataAdapter.UpdateCommand.Parameters.Add("@score", SqlDbType.Int, 0, "score");
        }

        private void SetupDB(DataSet dataSet, string tableName)
        {
            dataSet.Clear();
            dataAdapter.Fill(dataSet, tableName);
        }

        // 회원정보 입력하기
        public void InsertData(DataSet dataSet, string tableName, string iuid, string ipwd, string iname)
        {
            SetupDB(dataSet, tableName);
            DataRow newRow = dataSet.Tables[tableName].NewRow();
            newRow["uid"] = iuid;
            newRow["pwd"] = ipwd;
            newRow["name"] = iname;
            newRow["score"] = 0;
            dataSet.Tables[tableName].Rows.Add(newRow);

            dataAdapter.Update(dataSet, tableName);
        }

        // 최고점수 입력하기
        public void InsertData(DataSet dataSet, string tableName, string iuid, int iscore)
        {
            SetupDB(dataSet, tableName);

            string strFilter = "uid='" + iuid + "'";
            DataRow[] FindRow = dataSet.Tables[tableName].Select(strFilter);

            FindRow[0]["score"] = iscore;

            dataAdapter.Update(dataSet, tableName);
        }

        public string Login_ReadData(DataSet dataSet, string tableName, string lid)
        {
            SetupDB(dataSet, tableName);

            string strFilter = "uid='" + lid + "'";
            DataRow[] FindRow = dataSet.Tables[tableName].Select(strFilter);

            if (FindRow.Length == 0)
            {
                // 아이디가 존재하지 않는 경우 처리
                return null;
            }
            else
            {
                string dpwd = (string)FindRow[0]["pwd"];
                return dpwd;
            }
        }

        public string Check_ReadData(DataSet dataSet, string tableName, string cid)
        {
            SetupDB(dataSet, tableName);

            string strFilter = "uid='" + cid + "'";
            DataRow[] FindRow = dataSet.Tables[tableName].Select(strFilter);

            // 데이터 베이스에 아이디가 존재하지 않으면
            if (FindRow.Length == 0)
            {
                // 아이디가 존재하지 않는 경우 처리
                return "possible";
            }
            else
            {
                return "exist";
            }
        }
        // 유저 개인 최고점수 읽어오기
        public int Score_ReadData(DataSet dataSet, string tableName, string sid)
        {
            SetupDB(dataSet, tableName);

            string strFilter = "uid='" + sid + "'";
            DataRow[] FindRow = dataSet.Tables[tableName].Select(strFilter);

            if (FindRow.Length == 0)
            {
                // 아이디가 존재하지 않는 경우 처리
                return 0;
            }
            else
            {
                int bestscore = (int)FindRow[0]["score"];
                return bestscore;
            }
        }
        public string[] GetAllScores(DataSet dataSet, string tableName)
        {
            SetupDB(dataSet, tableName);

            // 배열 초기화
            string[] allScores = new string[dataSet.Tables[tableName].Rows.Count];

            // 이름과 점수 가져오기
            for (int i = 0; i < dataSet.Tables[tableName].Rows.Count; i++)
            {
                allScores[i] = dataSet.Tables[tableName].Rows[i]["name"] + ": " + dataSet.Tables[tableName].Rows[i]["score"];
            }

            // score 기준으로 내림차순 정렬
            Array.Sort(allScores, (a, b) => {
                int scoreA = int.Parse(a.Split(':')[1].Trim());
                int scoreB = int.Parse(b.Split(':')[1].Trim());
                return scoreB.CompareTo(scoreA);
            });

            return allScores;
        }
    }
}
