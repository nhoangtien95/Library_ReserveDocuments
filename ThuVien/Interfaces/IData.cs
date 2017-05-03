﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThuVien.Models;
using ThuVien.ViewModels;

namespace ThuVien.DAL
{
    public interface IData : IDisposable
    {
        Step1Model FirstData(Step1Model step1);
        Step1Model GetData();
        
        IEnumerable<SelectListItem> getHK();

        void InsertData(int tId, int sId, Step1Model step1, Step2Model step2);
    }
}