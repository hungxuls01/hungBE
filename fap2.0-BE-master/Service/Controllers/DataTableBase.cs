using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.Controllers
{
    public class DataTableBase<T> where T : class
    {
        private readonly List<T> _data;
        private readonly int _totalRecord;
        private readonly int _draw;

        public DataTableBase(List<T> data, int totalRecord, int draw)
        {
            _data = data;
            _totalRecord = totalRecord;
            _draw = draw;
        }

        public DataTableData<T> Init()
        {
            DataTableData<T> tableData = new DataTableData<T>();
            tableData.draw = _draw;
            tableData.data = _data;
            tableData.recordsTotal = _totalRecord;
            tableData.recordsFiltered = _totalRecord;
            return tableData;
        }
    }
}