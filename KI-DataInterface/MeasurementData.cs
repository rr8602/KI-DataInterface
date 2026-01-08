using System;
using System.Text;

namespace KI_DataInterface
{
    // 측정 데이터 송신 메시지 포맷
    public class SendMessage
    {
        // HEADER (사용자 입력)
        public string Company { get; set; }        // 2자리
        public string Plant { get; set; }          // 4자리
        public string Line { get; set; }           // 1자리
        public string Shop { get; set; }           // 2자리
        public string MachineCode { get; set; }    // 4자리
        public string Number { get; set; }         // 3자리

        // HEADER (측정 데이터)
        public int SEQ { get; set; }               // 4자리
        public string Vehicle { get; set; }        // 4자리
        public int BodyNo { get; set; }            // 6자리
        public int CheckDay { get; set; }          // 8자리 (YYYYMMDD)
        public int CheckTime { get; set; }         // 6자리 (HHMMSS)
        public int CycleTime { get; set; }         // 6자리
        public string TotalResult { get; set; }    // 2자리 ("OK" or "NG")

        // DATA (측정 결과 - 임시로 3개만)
        public MeasurementResult Result1 { get; set; }
        public MeasurementResult Result2 { get; set; }
        public MeasurementResult Result3 { get; set; }

        public SendMessage()
        {
            Result1 = new MeasurementResult();
            Result2 = new MeasurementResult();
            Result3 = new MeasurementResult();
        }

        // 문자열로 변환
        public string ToProtocolString()
        {
            StringBuilder sb = new StringBuilder();

            // HEADER
            sb.Append(PadRight(Company, 2));
            sb.Append(PadRight(Plant, 4));
            sb.Append(PadRight(Line, 1));
            sb.Append(PadRight(Shop, 2));
            sb.Append(PadRight(MachineCode, 4));
            sb.Append(PadRight(Number, 3));
            sb.Append(SEQ.ToString("D4"));           // 4자리 숫자
            sb.Append(PadRight(Vehicle, 4));
            sb.Append(BodyNo.ToString("D6"));        // 6자리 숫자
            sb.Append(CheckDay.ToString("D8"));      // 8자리 숫자
            sb.Append(CheckTime.ToString("D6"));     // 6자리 숫자
            sb.Append(CycleTime.ToString("D6"));     // 6자리 숫자
            sb.Append(PadRight(TotalResult, 2));

            // DATA - 3번까지만 구현
            sb.Append(Result1.ToProtocolString());
            sb.Append(Result2.ToProtocolString());
            sb.Append(Result3.ToProtocolString());

            // END
            sb.Append(";");

            return sb.ToString();
        }

        private string PadRight(string value, int length)
        {
            if (string.IsNullOrEmpty(value))
                return new string(' ', length);

            return value.Length >= length
                ? value.Substring(0, length)
                : value.PadRight(length);
        }
    }

    // 개별 측정 결과
    public class MeasurementResult
    {
        public string Result { get; set; }   // 1자리 (P/F/R/N)
        public string Detail { get; set; }   // 가변 (1번:50자리, 2번:10자리, 3번:50자리)

        public string ToProtocolString(int detailLength = 50)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(PadRight(Result, 1));
            sb.Append(PadRight(Detail, detailLength));
            return sb.ToString();
        }

        private string PadRight(string value, int length)
        {
            if (string.IsNullOrEmpty(value))
                return new string(' ', length);

            return value.Length >= length
                ? value.Substring(0, length)
                : value.PadRight(length);
        }
    }

    // 수신 메시지 파싱 (서버 응답)
    public class ReceiveMessage
    {
        public string Company { get; set; }
        public string Plant { get; set; }
        public string Line { get; set; }
        public string Shop { get; set; }
        public string MachineCode { get; set; }
        public string Number { get; set; }
        public string SEQ { get; set; }
        public string Vehicle { get; set; }
        public string BodyNo { get; set; }
        public string CheckDay { get; set; }
        public string CheckTime { get; set; }
        public string ReceiveResult { get; set; }  // P or F
        public string End { get; set; }

        public static ReceiveMessage msg { get; set; }

        // 수신한 문자열을 파싱
        public static ReceiveMessage Parse(string data)
        {
            if (string.IsNullOrEmpty(data) || data.Length < 41)
                return null;

            msg = new ReceiveMessage();
            int pos = 0;

            msg.Company = Extract(data, ref pos, 2);
            msg.Plant = Extract(data, ref pos, 4);
            msg.Line = Extract(data, ref pos, 1);
            msg.Shop = Extract(data, ref pos, 2);
            msg.MachineCode = Extract(data, ref pos, 4);
            msg.Number = Extract(data, ref pos, 3);
            msg.SEQ = Extract(data, ref pos, 4);
            msg.Vehicle = Extract(data, ref pos, 4);
            msg.BodyNo = Extract(data, ref pos, 6);
            msg.CheckDay = Extract(data, ref pos, 8);
            msg.CheckTime = Extract(data, ref pos, 6);
            msg.ReceiveResult = Extract(data, ref pos, 1);
            msg.End = Extract(data, ref pos, 1);

            return msg;
        }

        private static string Extract(string data, ref int position, int length)
        {
            if (position + length > data.Length)
                return string.Empty;

            string result = data.Substring(position, length).Trim();
            position += length;
            return result;
        }
    }
}
