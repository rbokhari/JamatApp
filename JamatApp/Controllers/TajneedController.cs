using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Jamat.DC;
using Jamat.EntityFramework;
using OfficeOpenXml;
using System.IO;
using System.Drawing;
using System.Web;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace JamatApp.Controllers
{
    [Authorize]
    public class TajneedController : ApiController
    {
        public ITajneedRepository _repo;

        public TajneedController(ITajneedRepository repository)
        {
            _repo = repository;
        }

        [Route("api/tajneed/")]
        [HttpGet]
        public Task<IQueryable<Tajneed>> Get()
        {
            var tajneeds = _repo.GetTajneedList();
            return tajneeds;
        }


        [Route("api/tajneed/GetTajneedByAuxilary/{id}")]
        [HttpGet]
        public Task<IQueryable<Tajneed>> GetTajneedByAuxilary(int id)
        {
            var tajneeds = _repo.GetTajneedListByAuxilaryId(id);
            return tajneeds;
        }

        [Route("api/tajneed/GetTajneedByNationality/{id}")]
        [HttpGet]
        public Task<IQueryable<Tajneed>> TajneedByNationality(int id)
        {
            // IQueryable filter data inside sql query and on database side get specified filter results only, 
            //where as IEnumerable get all data from databse and filter it on client side

            var tajneeds = _repo.GetTajneedListByNationalityId(id);
            return tajneeds;
        }

        [Route("api/tajneed/GetTajneedByRegion/{id}")]
        [HttpGet]
        public Task<IQueryable<Tajneed>> TajneedByRegion(int id)
        {
            // IQueryable filter data inside sql query and on database side get specified filter results only, 
            //where as IEnumerable get all data from databse and filter it on client side

            var tajneeds = _repo.GetTajneedListByRegionId(id);
            return tajneeds;
        }

        [Route("api/tajneed/GetTajneedByMosi")]
        [HttpGet]
        public Task<IQueryable<Tajneed>> TajneedByMosi()
        {
            // IQueryable filter data inside sql query and on database side get specified filter results only, 
            //where as IEnumerable get all data from databse and filter it on client side

            var tajneeds = _repo.GetTajneedListByMosi();
            return tajneeds;
        }

        [Route("api/tajneed/GetTajneedSearch/")]
        [HttpGet]
        public Task<IQueryable<Tajneed>> TajneedBySearch([FromUri]Tajneed search)
        {
            // IQueryable filter data inside sql query and on database side get specified filter results only, 
            //where as IEnumerable get all data from databse and filter it on client side

            var tajneeds = _repo.GetTajneedSearch(search);
            return tajneeds;
        }


        [Route("api/tajneed/getTajneedCount")]
        public Task<Int32> GetTajneedCount()
        {
            // IQueryable filter data inside sql query and on database side get specified filter results only, 
            //where as IEnumerable get all data from databse and filter it on client side

            return _repo.GetTajneedCount();
        }

        
        [Route("api/tajneed/getTajneedAuxilary")]
        public IQueryable<TajneedCount> GetTajneedAuxilaryCount()
        {
            // IQueryable filter data inside sql query and on database side get specified filter results only, 
            //where as IEnumerable get all data from databse and filter it on client side

            return _repo.GetTajneedAuxilaryCount();
        }

        
        [Route("api/tajneed/getTajneedRegion")]
        public IQueryable<TajneedCount> GetTajneedRegionCount()
        {
            // IQueryable filter data inside sql query and on database side get specified filter results only, 
            //where as IEnumerable get all data from databse and filter it on client side

            return _repo.GetTajneedRegionCount();
        }

        
        [Route("api/tajneed/getTajneedNationality")]
        public IQueryable<TajneedCount> GetTajneedNationalityCount()
        {
            // IQueryable filter data inside sql query and on database side get specified filter results only, 
            //where as IEnumerable get all data from databse and filter it on client side

            return _repo.GetTajneedNationalityCount();
        }

        
        [Route("api/tajneed/getTajneedWassiyat")]
        public IQueryable<TajneedCount> GetTajneedWassiyatCount()
        {
            // IQueryable filter data inside sql query and on database side get specified filter results only, 
            //where as IEnumerable get all data from databse and filter it on client side

            return _repo.GetTajneedWassiyatCount();
        }

        
        public IQueryable<Tajneed> Get(int id)
        {
            //IDepartmentsRepository _repo = new DepartmentRepository();
            var tajneed = _repo.GetTajneed(id);

            if (tajneed == null)
            {
                //Request.CreateErrorResponse(HttpStatusCode.BadRequest)
            }
            return tajneed;
        }

        [Route("api/tajneed/")]
        [HttpPost]
        public HttpResponseMessage Post([FromBody] Tajneed newTajneed)
        {
            if (ModelState.IsValid)
            {
                if (newTajneed.Id == 0)
                {
                    if (Request.Headers.Contains("userId"))
                    {
                        newTajneed.CreatedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                    }
                    newTajneed.CreatedOn = DateTime.Now;

                    if (_repo.AddTajneed(newTajneed) && _repo.Save())
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, newTajneed);
                    }
                }
                else
                {
                    if (Request.Headers.Contains("userId"))
                    {
                        newTajneed.ModifiedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                    }
                    newTajneed.ModifiedOn = DateTime.Now;

                    if (_repo.UpdateTajneed(newTajneed) && _repo.Save())
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, newTajneed);
                    }
                }

                return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
            }
            return null;
        }


        public HttpResponseMessage Put(int id, [FromBody] Tajneed updateTajneed)
        {
            //return Request.CreateResponse(HttpStatusCode.OK);
            if (ModelState.IsValid)
            {
                if (_repo.UpdateTajneed(updateTajneed) && _repo.Save())
                {
                    return Request.CreateResponse(HttpStatusCode.Created, updateTajneed);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
            }
            return null;
        }

        [ActionName("PostTajneedIncome")]
        [HttpPost]
        public HttpResponseMessage AddTajneedIncome([FromBody] TajneedIncome newIncome)
        {
            if (ModelState.IsValid)
            {
                if (newIncome.IncomeId == 0)
                {
                    //if (Request.Headers.Contains("userId"))
                    //{
                    //    newIncome.CreatedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                    //}
                    newIncome.CreatedOn = DateTime.UtcNow;

                    if (_repo.AddIncome(newIncome) && _repo.Save())
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, newIncome);
                        //return new HttpResponseMessage(HttpStatusCode.OK);
                    }
                }
                else if (newIncome.IncomeId != 0)
                {
                    //if (Request.Headers.Contains("userId"))
                    //{
                    //    newIncome.ModifiedBy = Convert.ToInt32(Request.Headers.GetValues("userId").First());
                    //}
                    newIncome.ModifiedOn = DateTime.Now;

                    if (_repo.UpdateIncome(newIncome) && _repo.Save())
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, newIncome);
                        //return new HttpResponseMessage(HttpStatusCode.OK);
                    }
                }

                return Request.CreateResponse(HttpStatusCode.InternalServerError, GetErrorMessages());
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
        }

        [Route("api/tajneed/getFile/{format}/")]
        [HttpGet]
        //[Authorize]
        public async Task<HttpResponseMessage> GetExcelFile([FromUri]Tajneed tajneed, string format)
        {

            var tajneeds = await _repo.GetTajneedList();

            switch (format)
            {
                case "pdf":
                    //return GetPdfFile(items);
                    return null;
                //if (ConvertXLSXtoPDF(fileName))
                //{
                //    return StreamAsFile(fileName.Replace(".xlsx",".pdf"));
                //}
                //break;
                case "xlsx":

                    var fileName = GetExcelDocument(tajneeds);
                    return StreamAsFile(fileName);
            }
            //}
            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }

        public static HttpResponseMessage StreamAsFile(string filename)
        {
            string filePath = String.Format("{0}\\temp\\{1}", HttpRuntime.AppDomainAppPath, filename);

            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(File.ReadAllBytes(filePath))
            };
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = filename
            };
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            File.Delete(filePath);
            return result;
        }

        private static string GetExcelDocument(IQueryable<Tajneed> tajneeds)
        {
            var selectedTajneed = tajneeds.Select(x => new
            {
                Code = x.TajneedCode,
                Name = x.FirstName,
                Father = x.FatherName,
                Husband = x.HusbandName,
                Mobile = x.MobileNumber,
                Wassiyat = x.WassiyatNumber,
                Auxilary = x.AuxilaryDetail.NameEn,
                Region = x.RegionDetail.RegionName,
                Nationality = x.NationalityDetail.NameEn
            });

            var xlPackage = new  ExcelPackage(new FileInfo(Guid.NewGuid() + ".xlsx")); 

            var oSheet = xlPackage.Workbook.Worksheets.Add("Item Listing");

            // Rows Heads

            oSheet.Cells["A4"].LoadFromCollection(selectedTajneed, true, OfficeOpenXml.Table.TableStyles.Medium4);


            oSheet.Cells.AutoFitColumns();

            // Report Heading
            oSheet.Cells["A1"].Value = "Jamat - Oman";
            oSheet.Cells["A1"].Style.Font.Size = 26;
            oSheet.Cells["A1"].Style.Font.Bold = true;
            oSheet.Cells["A1"].Style.Font.Color.SetColor(Color.DimGray);

            // Report Sub Heading
            oSheet.Cells["A2"].Value = "Tajneed Listing Report";
            oSheet.Cells["A2"].Style.Font.Size = 16;
            oSheet.Cells["A2"].Style.Font.Bold = true;
            oSheet.Cells["A2"].Style.Font.Color.SetColor(Color.Black);

            string fileName = Guid.NewGuid() + ".xlsx";
            string filePath = String.Format("{0}\\temp\\{1}", HttpRuntime.AppDomainAppPath, fileName);

            var stream = File.Create(filePath);
            xlPackage.SaveAs(stream);
            stream.Close();
            return fileName;

        }

        [HttpPost, Route("api/tajneed/upload")]
        [System.Web.Http.Authorize]
        public async Task<HttpResponseMessage> Upload()
        {
            try
            {
                if (!Request.Content.IsMimeMultipartContent())
                {
                    this.Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);
                }
                HttpPostedFile _file = HttpContext.Current.Request.Files[0];

                var provider = GetMultipartProvider();
                var result = await Request.Content.ReadAsMultipartAsync(provider);

                int paramData = GetFormData<int>(result, 0);
                int typeId = GetFormData<int>(result, 1);
                var memstream = new MemoryStream();
                _file.InputStream.CopyTo(memstream);
                
                var tajneedCard = new TajneedCard()
                {
                    TajneedId = paramData,
                    CardTypeId = typeId,
                    CardImage = memstream.ToArray(),
                    StatusId = 1,
                    CreatedBy = paramData,
                    CreatedOn = DateTime.Now
                };

                if (_repo.UpdateDocuments(tajneedCard) && _repo.Save())
                {
                    return Request.CreateResponse(HttpStatusCode.Created, tajneedCard);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, GetErrorMessages());
            }
        }

        public static byte[] compress(byte[] data)
        {
            using (MemoryStream outStream = new MemoryStream())
            {
                using (System.IO.Compression.GZipStream gzipStream =
                    new System.IO.Compression.GZipStream(outStream, System.IO.Compression.CompressionMode.Compress))
                using (MemoryStream srcStream = new MemoryStream(data))
                    srcStream.CopyTo(gzipStream);
                return outStream.ToArray();
            }
        }

        public static byte[] decompress(byte[] compressed)
        {
            using (MemoryStream inStream = new MemoryStream(compressed))
            using (System.IO.Compression.GZipStream gzipStream = new System.IO.Compression.GZipStream(inStream, System.IO.Compression.CompressionMode.Decompress))
            using (MemoryStream outStream = new MemoryStream())
            {
                gzipStream.CopyTo(outStream);
                return outStream.ToArray();
            }
        }

        private MultipartFormDataStreamProvider GetMultipartProvider()
        {
            var uploadFolder = "~/App_Data/Tmp/FileUploads"; // you could put this to web.config
            var root = HttpContext.Current.Server.MapPath(uploadFolder);
            Directory.CreateDirectory(root);
            return new MultipartFormDataStreamProvider(root);
        }

        // Extracts Request FormatData as a strongly typed model
        private int GetFormData<T>(MultipartFormDataStreamProvider result, short index)
        {
            if (result.FormData.HasKeys())
            {
                var unescapedFormData = Uri.UnescapeDataString(result.FormData.GetValues(index).FirstOrDefault() ?? String.Empty);
                if (!String.IsNullOrEmpty(unescapedFormData))
                {
                    //return JsonConvert.DeserializeObject<T>(unescapedFormData);
                    return Convert.ToInt32(unescapedFormData); // JsonConvert.DeserializeObject(unescapedFormData);
                }
            }
            return 0;
        }

        private string GetDeserializedFileName(MultipartFileData fileData)
        {
            var fileName = GetFileName(fileData);
            return JsonConvert.DeserializeObject(fileName).ToString();
        }

        public string GetFileName(MultipartFileData fileData)
        {
            return fileData.Headers.ContentDisposition.FileName;
        }

        private IEnumerable<string> GetErrorMessages()
        {
            return ModelState.Values.SelectMany(x => x.Errors.Select(e => e.ErrorMessage));
        }
        
    }
}
