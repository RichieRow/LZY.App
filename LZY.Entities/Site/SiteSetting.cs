﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LZY.Entities.Site
{
    /// <summary>
    /// 站点基本信息设置
    /// </summary>
    public class SiteSetting : EntityBase
    {
        /// <summary>
        /// 站点标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 站点描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 站点关键字
        /// </summary>
        public string Keywords { get; set; }
        /// <summary>
        /// 站点的LOGO
        /// </summary>
        public string Logo { get; set; }
        /// <summary>
        /// 版权信息
        /// </summary>
        public string Copyright { get; set; }
        /// <summary>
        /// 备案信息
        /// </summary>
        public string ICP { get; set; }
        /// <summary>
        /// 是否开启站点（关闭站点后，前端页面不可访问，后台不受影响）
        /// </summary>
        public bool IsOpen { get; set; }

        public bool UseImgLogo { get; set; }
        public string Statistics { get; set; }

        /// <summary>
        /// 打赏二维码
        /// </summary>
        public string QrCode { get; set; }
    }
}
