﻿using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Microsoft.Office.Interop.Word;
using Nest;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Support
    {
        public static async void DirSearch(string sDir, ElasticClient client)
        {
            //Console.WriteLine("DirSearch..(" + sDir + ")");
            try
            {
                //Console.WriteLine(sDir);

                foreach (string f in Directory.GetFiles(sDir))
                {
                    string[] listF = f.Split('\\');
                    string filenames = listF[listF.Length - 1];
                    string[] types = filenames.Split('.');
                    string noidung = "";

                    // đuôi file
                    string types2 = types[types.Length - 1];

                    // gọi hàm readfile để sử dụng

                    // khởi tạo file
                    var files = new Files
                    {
                        url = f,
                        filename = filenames,
                        body = noidung,
                        types = types2
                    };

                    var asyncIndexResponse = await client.IndexDocumentAsync(files);
                }

                foreach (string d in Directory.GetDirectories(sDir))
                {
                    DirSearch(d, client);
                }
            }
            catch (System.Exception excpt)
            {
                Console.WriteLine(excpt.Message);
            }
        }

        // này sẽ trả về những gì thì thứ 2 lên bàn sau
        // hiện tại nó sẽ in ra console
        public static async Task<List<Files>> SearchFile(string text, ElasticClient client)
        {
            // bool là so sánh , should giống như OR ...
            var searchResults = await client.SearchAsync<Files>(s => s

            .From(0)
            .Size(100)
            .Query(q => q
            .Bool(b => b
            .Should(sd => sd
            .Match(m => m.Field(f => f.url).Query(text)), sd => sd
            .Match(m => m.Field(f => f.body).Query(text)), sd => sd
            .Match(m => m.Field(f => f.filename).Query(text))
            ))));

            var result = searchResults.Documents;

            List<Files> resultList = new List<Files>();
            if (result.Count <= 1)
            {
                return null;
            }
            foreach (var f in result)
            {
                resultList.Add(f);
            }

            return resultList;
        }


        public string ReadFile(string url) {

            string[] listF = url.Split('\\');
            string filenames = listF[listF.Length - 1];
            string[] types = filenames.Split('.');
            string noidung = "";

            // đuôi file
            string types2 = types[types.Length - 1];

        
            if (types2 == "docx")
            {
                noidung = ReadFileWord(url); // vì f là url của file nên lấy f để đọc
            }
            else if (types2 == "xlsx")
            {
                noidung = ReadFileExcel(url);
            }
            else if (types2 == "pdf")
            {
                noidung = ReadFilePDF(url);
            }
            else if (types2 == "txt")
            {
                noidung = ReadFileTxt(url);
            }
            else
            {
                noidung = "";
            }
            return noidung;
        }

        public static async void Update(string url, ElasticClient client) {
            var searchResults = await client.SearchAsync<Files>(s => s
            .AllIndices()
            .From(0)
            .Size(200)
            .Query(q => q
            .Match(m => m
            .Field(f => f.url)
            .Query(url)))
            );

            var resultHit = searchResults.Hits.ToList();
            var rusultDoc = searchResults.Documents.ToList();
            string id = "";
            Files files = new Files();
            for (int i = 0; i < resultHit.Count; i++)
            {
                if (rusultDoc[i].url == url)
                {
                    id = resultHit[i].Id;
                    files = rusultDoc[i];
                }

            }

            if (id == "")
            {
                // do nothing
            }
            else
            {
                files.body = "da thay doi";

                var response = await client.UpdateAsync(DocumentPath<Files>
                    .Id(id),
                    u => u
                        .Index("filesmanager")
                        .DocAsUpsert(true)
                        .Doc(files));
            }
        }
        
        public static async void UpdateName(string oldurl,string newurl ,ElasticClient client) {

            //E:\learn\2020\Khởi nghiệp\\baitap_chuong4.pptx
            //string text = @"E:\learn\2020\Khởi nghiệp\baitap_chuong3.docx";
            var searchResults = await client.SearchAsync<Files>(s => s
            .AllIndices()
            .From(0)
            .Size(200)
            .Query(q => q
            .Match(m => m
            .Field(f => f.url)
            .Query(oldurl)))
            );

            var resultHit = searchResults.Hits.ToList();
            var rusultDoc = searchResults.Documents.ToList();
            string id = "";
            Files files = new Files();
            for (int i = 0; i < resultHit.Count; i++)
            {
                if (rusultDoc[i].url == oldurl)
                {
                    id = resultHit[i].Id;
                    files = rusultDoc[i];
                }

            }

            if (id == "")
            {
                // do nothing
            }
            else {
                files.url = newurl;

                var response = await client.UpdateAsync(DocumentPath<Files>
                    .Id(id),
                    u => u
                        .Index("filesmanager")
                        .DocAsUpsert(true)
                        .Doc(files));
            }


            

        }



        public static async void Create(string url, ElasticClient client) {
            string[] listF = url.Split('\\');
            string filenames = listF[listF.Length - 1];
            string[] types = filenames.Split('.');


            // đuôi file
            string types2 = types[types.Length - 1];
            string noidung = "";
            // hiện tại test chưa cần nội dung
            // nên phần này bỏ đi
            //string noidung = ReadFile(f, types2);


            // khởi tạo file
            var files = new Files
            {
                url = url,
                filename = filenames,
                body = noidung,
                types = types2
            };

            var asyncIndexResponse = await client.IndexDocumentAsync(files);

        }
        // Hàm delete
        public static async void Delete(string url, ElasticClient client)
        {
            

            //E:\learn\2020\Khởi nghiệp\\baitap_chuong4.pptx
            //string text = @"E:\learn\2020\Khởi nghiệp\baitap_chuong4.pptx";
            var searchResults = await client.SearchAsync<Files>(s => s
            .AllIndices()
            .From(0)
            .Size(200)
            .Query(q => q
            .Match(m => m
            .Field(f => f.url)
            .Query(url)))
            );

            var resultHit = searchResults.Hits.ToList();
            var rusultDoc = searchResults.Documents.ToList();
            string id = "";
            for (int i = 0; i < resultHit.Count; i++)
            {
                if (rusultDoc[i].url == url)
                {
                    id = resultHit[i].Id;
                }


            }
            //string dl = @"{/filesmanager/_doc/yoz7ZHUBLyuMYkFKjSMh}";
            //var rsd = await client.DeleteAsync<Files>(dl);

            if (id == "")
            {
                // do nothing
            }
            else
            {
                var response = await client.DeleteAsync<Files>(id, d => d.Index("filesmanager"));
            }

            

            

        }

        // read file word
        public static string ReadFileWord(string url)
        {
            Application application = new Application();
            // read the file with url
            Document document = application.Documents.Open(url);

            // read line
            string body = "";
            for (int i = 1; i <= document.Paragraphs.Count; i++)
            {
                // Write the word.

                body = body + " " + document.Paragraphs[i].Range.Text.Trim();
            }

            // Close word.
            document.Close();
            //kill
            application.Quit();
            return body;
        }

        // read file excel
        public static string ReadFileExcel(string url)
        {
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(url);

            Microsoft.Office.Interop.Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Microsoft.Office.Interop.Excel.Range xlRange = xlWorksheet.UsedRange;

            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;
            string body = "";

            //iterate over the rows and columns and print to the console as it appears in the file
            //excel is not zero based!!
            for (int i = 1; i <= rowCount; i++)
            {
                for (int j = 1; j <= colCount; j++)
                {
                    // vì chỉ là body nên không xuống dòng gì cả
                    if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                        body = body + " " + xlRange.Cells[i, j].Value2.ToString();
                }
            }

            xlWorkbook.Close();
            xlApp.Quit();
            return body;
        }

        // read file pdf
        public static string ReadFilePDF(string url)
        {
            PdfReader reader = new PdfReader(url);

            string body = "";
            for (int page = 1; page <= reader.NumberOfPages; page++)
            {
                string text = PdfTextExtractor.GetTextFromPage(reader, page);
                var a = text.Split(' ', '\n');

                for (int i = 0; i < a.Length; i++)
                {
                    body = body + " " + a[i];
                }
            }
            reader.Close();

            return body;
        }

        // read file txt
        public static string ReadFileTxt(string url)
        {
            string text = File.ReadAllText(url);

            return text;
        }
    }
}