using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SCGP.COA.BUSINESSLOGIC.Commands.Laminate.Interface;
using SCGP.COA.BUSINESSLOGIC.Models;
using SCGP.COA.COMMON.Attributes;
using SCGP.COA.COMMON.Contants;
using SCGP.COA.COMMON.Models;
using SCGP.COA.DATAACCESS.Models;

namespace SCGP.COA.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiException]
    public class LaminateController : ControllerBase
    {
        private ILaminateCommand _laminateCommand;
        public LaminateController(ILaminateCommand laminateCommand)
        {
            _laminateCommand = laminateCommand;
        }
        [HttpGet]
       // [Authorize(Roles = RoleConstant.PrintCoaExport)]
        public ResponseResult<List<MasterLabDataModel>> DisplayLab()
        {
            var result = _laminateCommand.GetMasterLabData();
            return ResponseResult<List<MasterLabDataModel>>.Success(result);
        }
        [HttpPost]
       // [Authorize(Roles = RoleConstant.PrintCoaExport)]
        public ResponseResult<SearchResModel<DataLabModel>> Search([FromBody] SearchReqModel<DataLabModel> param)
        {
            var data = _laminateCommand.SearchDisplayLab(param);
            return ResponseResult<SearchResModel<DataLabModel>>.Success(data);
        }

        [HttpPost]
      // [Authorize(Roles = RoleConstant.PrintCoaExport)]
        public ResponseResult<List<DataLabModel>> UploadLab([FromBody] List<DataLabModel> param)
        {
            try 
            {
                var result = _laminateCommand.SetExcelToDataTable(param);
                return ResponseResult<List<DataLabModel>>.Success(result);
            }
            catch (Exception ex)
            {
                return ResponseResult<List<DataLabModel>>.Fail(ex.Message);
            }     
       
        }
        [HttpGet]
        //[Authorize(Roles = RoleConstant.PrintCoaExport)]
        public ResponseResult<FileDataModel> PrintExportExcel()
        {
            var result = _laminateCommand.PrintExportExcel();
            return ResponseResult<FileDataModel>.Success(result);
        }

    }
}
