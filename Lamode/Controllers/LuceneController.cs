using Lamode.Models;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Lamode.Controllers
{
    public class LuceneController : Controller
    {
        static readonly Lucene.Net.Util.Version _version = Lucene.Net.Util.Version.LUCENE_29;
        private static Lucene.Net.Store.Directory directory;
        private static Analyzer analyser;
        private static IndexWriter writer;
        private static IndexReader reader;
        private static IndexSearcher searcher;
        private const string FieldName = "text";
        private static readonly Lucene.Net.Util.Version LuceneVersion = Lucene.Net.Util.Version.LUCENE_CURRENT;
        // GET: Lucene
        public ActionResult Index()
        {
            directory = new RAMDirectory();
            analyser = new SimpleAnalyzer();
            writer = new IndexWriter(directory, analyser, true, IndexWriter.MaxFieldLength.LIMITED);
            writer.AddDocument(CreateDocument("LaMode"));
            writer.Commit();
            writer.Close();

            return View();
        }

        public JsonResult Search(string text)
        {
            reader = IndexReader.Open(directory, true);
            searcher = new IndexSearcher(reader);
            return Json(Search(searcher, analyser, text));
        }

        private object Search(IndexSearcher searcher, Analyzer analyser, string searchString)
        {
            var parser = new QueryParser(LuceneVersion, FieldName, analyser);
            Query q = parser.Parse(searchString);
            TopDocs topDocs = searcher.Search(q, null, 5, Sort.RELEVANCE);
            var result = "Result found: " + topDocs.TotalHits + "Document(s) matching the query." + q;
            List<string> matches = new List<string>();
            foreach(ScoreDoc match in topDocs.ScoreDocs)
            {
                Document doc = searcher.Doc(match.Doc);
                matches.Add("we matched" + doc.Get(FieldName));

            }
            searcher.Close();
            return new Result { ResultList = matches, ResultString = result };


        }

        private Document CreateDocument(string contact)
        {
            var document = new Document();
            document.Add(new Field(FieldName, contact, Field.Store.YES, Field.Index.ANALYZED));
            return document;

        }
       
    }
}
public class Result
{
    public string ResultString { get; set; }
    public List<string> ResultList { get; set; }
}