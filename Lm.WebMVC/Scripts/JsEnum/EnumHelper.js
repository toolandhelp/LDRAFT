// JavaScript Document
/*
JavaScript脚本命名法规则如下
(1)变量采用小驼峰法命名法：除第一个单词之外，其他单词首字母大写。
(2)方法采用大驼峰法命名法：相比小驼峰法，大驼峰法把第一个单词的首字母也大写了。
(3)命名采用有效意义命名，尽可能采用全称。
*/
/*javascript中null,undefined,0,"",false作为if的条件的时候，被认为是flase*/
/*javascript除了数字，布尔，字符串这些原始值和null, undefined这些特殊的，其他都是对象*/
/**
 * 旨在解决常规性业务通用功能。
 */
; !function (window, undefined) {
    "use strict";
    window.Enum = {
        version: "1.0.0",
        //文件类型
        FileContentType: function () {
            var stageEnum = {
                /// <summary>
                /// word文档
                /// </summary>
                Word: 1,
                /// <summary>
                /// excel文件
                /// </summary>
                Excel: 2,
                /// <summary>
                /// ppt文件
                /// </summary>
                PPT: 3,
                /// <summary>
                /// 文本文档
                /// </summary>
                TXT: 4,
                /// <summary>
                /// 图片
                /// </summary>
                Image: 5,
                /// <summary>
                /// pdf文件
                /// </summary>
                Pdf: 6,
                /// <summary>
                /// 网页文件
                /// </summary>
                Html: 7,
                /// <summary>
                /// flash文件
                /// </summary>
                SWF: 8,
                /// <summary>
                /// 音频文件
                /// </summary>
                Audio: 9,
                /// <summary>
                /// 视频文件
                /// </summary>
                Video: 10,
                /// <summary>
                /// 其他文件
                /// </summary>
                Other: 99
            };
            return stageEnum;
        },
        //文件类型
        FormatJSONFileContentType: function (s) {
            var stageEnum = this.FileContentType();
            switch (s) {
                case stageEnum.Word: return "Word文档";
                case stageEnum.Excel: return "Excel文件";
                case stageEnum.PPT: return "PPT文件";
                case stageEnum.TXT: return "文本文档";
                case stageEnum.Image: return "图片";
                case stageEnum.Pdf: return "PDF文件";
                case stageEnum.Html: return "网页文件";
                case stageEnum.SWF: return "FLASH文件";
                case stageEnum.Audio: return "音频文件";
                case stageEnum.Video: return "视频文件";
                case stageEnum.Other: return "其他";
                default: return "未知";
            }
        },
        //数据状态
        StageType: function () {
            var stageEnum = {
                /// <summary>
                /// 正常
                /// </summary>
                Normal: 1,
                /// <summary>
                /// 停用
                /// </summary>
                Disable: -1
            };
            return stageEnum;
        },
        //数据状态
        FormatJSONStageType: function (s) {
            var stageEnum = this.StageType();
            switch (s) {
                case stageEnum.Normal: return "正常";
                case stageEnum.Disable: return "停用";
                default: return "未知";
            }
        },
        //办公室数据状态
        JDBType: function () {
            var stageEnum = {
                /// <summary>
                /// 党政办公室
                /// </summary>
                Dzbgs: 1,
                /// <summary>
                /// 社区党建办公室
                /// </summary>
                Sqdjbgs: 2,
                /// <summary>
                /// 党群办公室
                /// </summary>
                Dqbgs: 3,
                /// <summary>
                /// 社区管理办公室
                /// </summary>
                Sqglbgs: 4,
                /// <summary>
                /// 社区服务办公室
                /// </summary>
                Sqfwbgs: 5,
                /// <summary>
                /// 社区平安办公室
                /// </summary>
                Sqpabgs: 6,
                /// <summary>
                /// 社区自治办公室
                /// </summary>
                Sqzzbgs: 7,
                /// <summary>
                /// 社区发展办公室
                /// </summary>
                Sqfzbgs:8,
            };
            return stageEnum;
        },
        //办公室数据状态
        FormatJSONJDBType: function (s) {
            var stageEnum = this.JDBType();
            switch (s) {
                case stageEnum.Dzbgs: return "党政办公室";
                case stageEnum.Sqdjbgs: return "社区党建办公室";
                case stageEnum.Dqbgs: return "党群办公室";
                case stageEnum.Sqglbgs: return "社区管理办公室";
                case stageEnum.Sqfwbgs: return "社区服务办公室";
                case stageEnum.Sqpabgs: return "社区平安办公室";
                case stageEnum.Sqzzbgs: return "社区自治办公室";
                case stageEnum.Sqfzbgs: return "社区发展办公室";

                default: return "未知";
            }
        },

        //街道对接人员身份
        StreetEmployeeType: function () {
            var stageEnum = {
                /// <summary>
                /// 分管领导
                /// </summary>
                Leader: 1,
                /// <summary>
                /// 协管负责人
                /// </summary>
                Assistant: 2,
                /// <summary>
                /// 责任人AB角
                /// </summary>
                People: 3
            };
            return stageEnum;
        },
        //街道对接人员身份
        FormatJSONStreetEmployeeType: function (s) {
            var stageEnum = this.StreetEmployeeType();
            switch (s) {
                case stageEnum.Leader: return "分管领导";
                case stageEnum.Assistant: return "协管负责人";
                case stageEnum.People: return "责任人AB角";
                default: return "未知";
            }
        },
        //部门对接人员身份
        DeptEmployeeType: function () {
            var stageEnum = {
                /// <summary>
                /// 分管领导
                /// </summary>
                Leader: 4,
                /// <summary>
                /// 内设机构负责人
                /// </summary>
                Assistant:5,
                /// <summary>
                /// 责任人AB角
                /// </summary>
                People: 6
            };
            return stageEnum;
        },
        //部门对接人员身份
        FormatJSONDeptEmployeeType: function (s) {
            var stageEnum = this.DeptEmployeeType();
            switch (s) {
                case stageEnum.Leader: return "分管领导";
                case stageEnum.Assistant: return "内设机构负责人";
                case stageEnum.People: return "责任人AB角";
                default: return "未知";
            }
        },

        //数据状态
        MailFlag: function () {
            var stageEnum = {
                /// <summary>
                /// 草稿箱
                /// </summary>
                DraftBox: 1,
                /// <summary>
                /// 发件箱
                /// </summary>
                OutBox: 2,
                /// <summary>
                /// 收件箱
                /// </summary>
                InBox: 3,
                /// <summary>
                /// 已删除
                /// </summary>
                Deleted: 4
            };
            return stageEnum;
        },
        //数据状态
        MailState: function () {
            var stageEnum = {
                /// <summary>
                /// 未读
                /// </summary>
                Unread: 1,
                /// <summary>
                /// 已读
                /// </summary>
                Read: 2,
                /// <summary>
                /// 删除
                /// </summary>
                Reply: 3,
                /// <summary>
                /// 删除
                /// </summary>
                Remove: 4,
                /// <summary>
                /// 彻底删除
                /// </summary>
                Delete: 9
            };
            return stageEnum;
        },
        //合同状态
        ContractState: function () {
            var stageEnum = {
                /// <summary>
                /// 已签订
                /// </summary>
                Sign: 1,
                /// <summary>
                /// 已收回
                /// </summary>
                Back: 2,
                /// <summary>
                /// 已完成
                /// </summary>
                Finish: 3
            };
            return stageEnum;
        },
        //合同状态
        FormatJSONContractState: function (s) {
            var stageEnum = this.ContractState();
            switch (s) {
                case stageEnum.Sign: return "已签订";
                case stageEnum.Back: return "已收回";
                case stageEnum.Finish: return "已完成";
                default: return "未知";
            }
        },
        //发票状态
        InvoiceState: function () {
            var stageEnum = {
                /// <summary>
                /// 已签订
                /// </summary>
                Billing: 1,
                /// <summary>
                /// 已收回
                /// </summary>
                Pay: 2
            };
            return stageEnum;
        },
        //发票状态
        FormatJSONInvoiceState: function (s) {
            var stageEnum = this.InvoiceState();
            switch (s) {
                case stageEnum.Billing: return "已开票";
                case stageEnum.Pay: return "已支付";
                default: return "未知";
            }
        }
    };

    //for 页面模块加载、Node.js运用、页面普通应用
    "function" === typeof define ? define(function () {
        return Enum;
    }) : "undefined" != typeof exports ? module.exports = Enum : window.Enum = Enum;
}(window);
