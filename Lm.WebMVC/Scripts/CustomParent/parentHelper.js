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
    window.ParentHelper = {
        version: "1.0.0",
        ShowWorkDialogInfo: function (EmpId) {
            $("#dvDialog1").children().appendTo($("#dataArea"));
            var objId = "tb_work_" + EmpId;
            var tb = $("#" + objId);
            if (!tb || !tb.length) {
                var layerLoadIdx;
                $.ajax({
                    url: '/Index/Home/LoadWorkInfoJson',
                    type: 'POST',
                    data: { EmpID: EmpId },
                    dataType: "json",
                    beforeSend: function () { layerLoadIdx = MISSY.iShowLoading("加载中，请稍候...") },
                    ContentType: "application/json;charset=utf-8",
                    success: function (response) {
                        if (!response) { MISSY.iErrorReturnNull(); }
                        var data = response.Data || [];
                        var buf = [];
                        buf.push("<div id=\"" + objId + "\" class=\"table_area\" style=\"width:800px;\"><table class=\"tb_fix\"><thead><tr><th >序号</th><th>街道内设结构</th><th>项目名称</th><th>具体内容</th></tr></thead><tfoot><tr><th></th><th></th><th></th><th></th></tr></tfoot></table><div class=\"content_area\"><table class=\"tb_data\"><tbody>");

                        var rowIdx = 0;
                        if (data.length) {
                            $(data).each(function (idx1, item) {
                                rowIdx++;
                                buf.push("<tr><td>");
                                buf.push(rowIdx);
                                buf.push("</td><td>");
                                buf.push(item.Ins_Name || "");
                                buf.push("</td><td>");
                                buf.push(item.Project_Name || "");
                                buf.push("</td><td style=\"text-align:left;\">");
                                buf.push(item.Project_Content || "");
                                buf.push("</td></tr>");
                            })
                        } else {
                            buf.push("<tr><td colspan=\"4\">没有可供展示的数据.</td></tr>");
                        }
                        buf.push("</tbody><tfoot><tr><th style=\"width:26px\"></th><th style=\"width:150px;\"></th><th style=\"width:200px;\"></th><th></th></tr></tfoot></table></div>")
                        $("#dvDialog1").append(buf.join(""));
                        var viewHeight = $("#dvDialog1").height();
                        MISSY.iShowDialog("dvDialog1", "条块之间人员工作职责明细表");

                        var fixHeight = $("#" + objId + " table.tb_fix").height();
                        var areaHeight = $("#" + objId + " .content_area").height();
                        $("#" + objId + " .content_area").height(viewHeight - fixHeight - 10);
                        $("#" + objId).css("padding-top", fixHeight + "px");
                        if (areaHeight > viewHeight - fixHeight - 10) {
                            $("#" + objId + " table.tb_fix,#" + objId + " table.tb_data").width($("#" + objId + "").width() - 20);
                        }
                        $("#" + objId + " table.tb_fix tfoot tr th").each(function (idx, item) {
                            var dataTh = $("#" + objId + " table.tb_data tfoot tr th:eq(" + idx + ")");
                            var width = dataTh.outerWidth();
                            dataTh.outerWidth(width)
                            $(item).outerWidth(width);
                        })
                    },
                    error: function (xmlHttpRequest, textStatus, errorThrown) {
                        MISSY.iDebugAjaxError(xmlHttpRequest, textStatus, errorThrown);
                        MISSY.iErrorMessage("数据加载失败,请稍后再试.");
                    },
                    complete: function (xmlHttpRequest, textStatus) {
                        MISSY.iHideLoading(layerLoadIdx);
                    }
                });
            } else {
                tb.appendTo($("#dvDialog1"));
                MISSY.iShowDialog("dvDialog1", "条块之间人员工作职责明细表");
            }
        },
        ShowDialogInfo: function (searchType, searchCode, DeptCode, searchProject) {
            $("#dvDialog").children().appendTo($("#dataArea"));
            var objId = "tb_" + searchType + "_" + searchCode + "_" + DeptCode + "_" + searchProject;
            var tb = $("#" + objId);
            if (!tb || !tb.length) {
                searchType === 1 ? this.BuilderStreetDetails(objId, searchCode, DeptCode, searchProject) : searchType === 2 ? this.BuilderKSDetails(objId, DeptCode, searchProject) : this.BuilderDeptDetails(objId, DeptCode, searchProject);
            } else {
                tb.appendTo($("#dvDialog"));
                MISSY.iShowDialog("dvDialog", "条块之间工作职责人员对接明细表");
            }
        },
        BuilderStreetDetails: function (objId, searchCode, DeptCode, searchProject) {
            var layerLoadIdx;
            $.ajax({
                url: '/Index/Home/LoadStreetInfoJson',
                type: 'POST',
                data: { Code: searchCode, Dept: DeptCode, Project: searchProject },
                dataType: "json",
                beforeSend: function () { layerLoadIdx = MISSY.iShowLoading("加载中，请稍候...") },
                ContentType: "application/json;charset=utf-8",
                success: function (response) {
                    if (!response) { MISSY.iErrorReturnNull(); }
                    switch (response.ErrorType)//标记
                    {
                        case 0://错误
                            MISSY.iErrorMessage(response.MessageContent);
                            return gridData;
                        case 1://返回正确数据
                            break;
                        case 3://未登录
                            MISSY.iNoLogin(response.MessageContent);
                            return;
                        default:
                            MISSY.iErrorMessage(response.MessageContent);
                            return gridData;
                    }
                    var data = response.Data || [];
                    var buf = [];
                    if (MISSY.IsEmpty(DeptCode)) {
                        buf.push("<div id=\"" + objId + "\" class=\"table_area\"><table class=\"tb_fix\"><thead><tr><th rowspan=\"2\">序号</th><th colspan=\"2\">工作职责</th><th rowspan=\"2\">业务指导部门</th><th rowspan=\"2\">分管领导</th><th rowspan=\"2\">内设机构</th><th rowspan=\"2\">内设机构<br />负责人</th><th rowspan=\"2\">联系电话</th><th rowspan=\"2\">责任人<br />AB角</th><th rowspan=\"2\">联系电话</th></tr><tr><th>项目名称</th><th>具体内容</th></tr></thead><tfoot><tr><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr></tfoot></table><div class=\"content_area\"><table class=\"tb_data\"><tbody>");
                    } else
                        buf.push("<div id=\"" + objId + "\" class=\"table_area\"><table class=\"tb_fix\"><thead><tr><th rowspan=\"2\">序号</th><th colspan=\"2\">工作职责</th><th colspan=\"6\">街道</th><th colspan=\"7\">部门</th></tr><tr><th>项目名称</th><th>具体内容</th><th>分管领导<br />(处级)</th><th>内设机构</th><th>协管<br />负责人<br />(科级)</th><th>联系电话</th><th>责任人<br />AB角<br />(承办人)</th><th>联系电话</th><th>业务<br />指导部门</th><th>分管领导</th><th>内设机构</th><th>内设机构<br />负责人</th><th>联系电话</th><th>责任人<br />AB角</th><th>联系电话</th></tr></thead><tfoot><tr><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr></tfoot></table><div class=\"content_area\"><table class=\"tb_data\"><tbody>");
                    var rowIdx = 0;
                    $(data).each(function (idx1, project) {
                        rowIdx++;
                        var rowCount = 1;
                        if (project.Contents && project.Contents.length && project.Contents[0].DeptCount) {
                            rowCount = project.Contents[0].DeptCount;
                        }
                        buf.push("<tr><td rowspan=\"" + rowCount + "\">" + rowIdx + "</td>");
                        buf.push("<td rowspan=\"" + project.ContentCount + "\">" + project.Project + "</td>");
                        if (project.ContentCount) {
                            $(project.Contents).each(function (idx2, content) {
                                if (idx2 > 0) {
                                    rowIdx++;
                                    buf.push("</tr><tr><td rowspan=\"" + content.DeptCount + "\">" + rowIdx + "</td>");
                                }
                                buf.push("<td rowspan=\"" + content.DeptCount + "\" style=\"text-align: left;\">" + content.Content + "</td>");
                                if (content.DeptCount) {
                                    if (content.StreetContent) {
                                        var entity = content.StreetContent;
                                        buf.push("<td rowspan=\"" + content.DeptCount + "\">" + (entity.Leader || "") + "</td>");
                                        buf.push("<td rowspan=\"" + content.DeptCount + "\">" + (entity.InsName || "") + "</td>");
                                        buf.push("<td rowspan=\"" + content.DeptCount + "\">" + (entity.Assistant || "") + "</td>");
                                        buf.push("<td rowspan=\"" + content.DeptCount + "\">" + (entity.AssistantPhone || "") + "</td>");
                                        buf.push("<td rowspan=\"" + content.DeptCount + "\">" + (entity.People || "") + "</td>");
                                        buf.push("<td rowspan=\"" + content.DeptCount + "\">" + (entity.PeoplePhone || "") + "</td>");
                                    } else//没有街道对接人员时
                                        if (!MISSY.IsEmpty(DeptCode))
                                            buf.push("<td rowspan=\"" + content.DeptCount + "\"></td><td rowspan=\"" + content.DeptCount + "\"></td><td rowspan=\"" + content.DeptCount + "\"></td><td rowspan=\"" + content.DeptCount + "\"></td><td rowspan=\"" + content.DeptCount + "\"></td><td rowspan=\"" + content.DeptCount + "\"></td>");

                                    if (content.DeptContents && content.DeptContents.length) {
                                        $(content.DeptContents).each(function (idx3, item) {
                                            if (idx3 > 0) {
                                                buf.push("</tr><tr>");
                                            }
                                            buf.push("<td>" + (item.Dept || "") + "</td>");
                                            buf.push("<td>" + (item.Leader || "") + "</td>");
                                            buf.push("<td>" + (item.InsName || "") + "</td>");
                                            buf.push("<td>" + (item.Assistant || "") + "</td>");
                                            buf.push("<td>" + (item.AssistantPhone || "") + "</td>");
                                            buf.push("<td>" + (item.People || "") + "</td>");
                                            buf.push("<td>" + (item.PeoplePhone || "") + "</td>");
                                        })
                                    } else//没有部门对接人员时
                                        buf.push("<td></td><td></td><td></td><td></td><td></td><td></td><td></td>");

                                } else//没有对接人员时
                                    if (!MISSY.IsEmpty(DeptCode))
                                        buf.push("<td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td>");
                                    else
                                        buf.push("<td></td><td></td><td></td><td></td><td></td><td></td><td></td>");
                            })
                        } else//没有具体内容时
                            if (!MISSY.IsEmpty(DeptCode))
                                buf.push("<td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td>");
                            else
                                buf.push("<td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td>");
                        buf.push("</tr>");
                    })
                    if (MISSY.IsEmpty(DeptCode))
                        buf.push("</tbody><tfoot><tr><th style=\"width:26px\"></th><th style=\"width:76px\"></th><th style=\"width:160px\"></th><th></th><th style=\"width:60px\"></th><th></th><th style=\"width:60px\"></th><th style=\"width:80px\"></th><th style=\"width:60px\"></th><th style=\"width:80px\"></th></tr></tfoot></table></div>");
                    else
                        buf.push("</tbody><tfoot><tr><th style=\"width:26px\"></th><th style=\"width:76px\"></th><th style=\"width:160px\"></th><th style=\"width:60px\"></th><th></th><th style=\"width:60px\"></th><th style=\"width:80px\"></th><th style=\"width:60px\"></th><th style=\"width:80px\"></th><th></th><th style=\"width:60px\"></th><th></th><th style=\"width:60px\"></th><th style=\"width:80px\"></th><th style=\"width:60px\"></th><th style=\"width:80px\"></th></tr></tfoot></table></div>");
                    $("#dvDialog").append(buf.join(""));

                    MISSY.iShowDialog("dvDialog", "条块之间工作职责人员对接明细表");

                    var viewHeight = $("#dvDialog").height();
                    var fixHeight = $("#" + objId + " table.tb_fix").height();
                    var areaHeight = $("#" + objId + " .content_area").height();
                    $("#" + objId + " .content_area").height(viewHeight - fixHeight - 10);
                    $("#" + objId).css("padding-top", fixHeight + "px");
                    if (areaHeight > viewHeight - fixHeight - 10) {
                        $("#" + objId + " table.tb_fix,#" + objId + " table.tb_data").width($("#" + objId + "").width() - 20);
                    }
                    $("#" + objId + " table.tb_fix tfoot tr th").each(function (idx, item) {
                        var dataTh = $("#" + objId + " table.tb_data tfoot tr th:eq(" + idx + ")");
                        var width = dataTh.outerWidth();
                        dataTh.outerWidth(width)
                        $(item).outerWidth(width);
                    })
                },
                error: function (xmlHttpRequest, textStatus, errorThrown) {
                    MISSY.iDebugAjaxError(xmlHttpRequest, textStatus, errorThrown);
                    MISSY.iErrorMessage("数据加载失败,请稍后再试.");
                },
                complete: function (xmlHttpRequest, textStatus) {
                    MISSY.iHideLoading(layerLoadIdx);
                }
            });
        },
        BuilderDeptDetails: function (objId, DeptCode, searchProject) {
            var layerLoadIdx;
            $.ajax({
                url: '/Index/Home/LoadDeptInfoJson',
                type: 'POST',
                data: { Dept: DeptCode, Project: searchProject },
                dataType: "json",
                beforeSend: function () { layerLoadIdx = MISSY.iShowLoading("加载中，请稍候...") },
                ContentType: "application/json;charset=utf-8",
                success: function (response) {
                    if (!response) { MISSY.iErrorReturnNull(); }
                    switch (response.ErrorType)//标记
                    {
                        case 0://错误
                            MISSY.iErrorMessage(response.MessageContent);
                            return gridData;
                        case 1://返回正确数据
                            break;
                        case 3://未登录
                            MISSY.iNoLogin(response.MessageContent);
                            return;
                        default:
                            MISSY.iErrorMessage(response.MessageContent);
                            return gridData;
                    }
                    var data = response.Data || [];
                    var buf = ["<div id=\"" + objId + "\" class=\"table_area\"><table class=\"tb_fix\"><thead><tr><th rowspan=\"2\">序号</th><th colspan=\"2\">工作职责</th><th rowspan=\"2\">业务指导部门</th><th rowspan=\"2\">分管领导</th><th rowspan=\"2\">内设机构</th><th rowspan=\"2\">内设机构<br />负责人</th><th rowspan=\"2\">联系电话</th><th rowspan=\"2\">责任人<br />AB角</th><th rowspan=\"2\">联系电话</th></tr><tr><th>项目名称</th><th>具体内容</th></tr></thead><tfoot><tr><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr></tfoot></table><div class=\"content_area\"><table class=\"tb_data\"><tbody>"];
                    var rowIdx = 0;
                    $(data).each(function (idx1, project) {
                        rowIdx++;
                        var rowCount = 1;
                        if (project.Contents && project.Contents.length && project.Contents[0].DeptCount) {
                            rowCount = project.Contents[0].DeptCount;
                        }
                        buf.push("<tr><td rowspan=\"" + rowCount + "\">" + rowIdx + "</td>");
                        buf.push("<td rowspan=\"" + project.ContentCount + "\">" + project.Project + "</td>");
                        if (project.ContentCount) {
                            $(project.Contents).each(function (idx2, content) {
                                if (idx2 > 0) {
                                    rowIdx++;
                                    buf.push("</tr><tr><td rowspan=\"" + content.DeptCount + "\">" + rowIdx + "</td>");
                                }
                                buf.push("<td rowspan=\"" + content.DeptCount + "\" style=\"text-align: left;\">" + content.Content + "</td>");
                                if (content.DeptContents && content.DeptContents.length) {
                                    $(content.DeptContents).each(function (idx3, item) {
                                        if (idx3 > 0) {
                                            buf.push("</tr><tr>");
                                        }
                                        buf.push("<td>" + (item.Dept || "") + "</td>");
                                        buf.push("<td>" + (item.Leader || "") + "</td>");
                                        buf.push("<td>" + (item.InsName || "") + "</td>");
                                        buf.push("<td>" + (item.Assistant || "") + "</td>");
                                        buf.push("<td>" + (item.AssistantPhone || "") + "</td>");
                                        buf.push("<td>" + (item.People || "") + "</td>");
                                        buf.push("<td>" + (item.PeoplePhone || "") + "</td>");
                                    })
                                } else//没有部门对接人员时
                                    buf.push("<td></td><td></td><td></td><td></td><td></td><td></td><td></td>");
                            })
                        } else//没有具体内容时
                            buf.push("<td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td>");
                        buf.push("</tr>");
                    })
                    buf.push("</tbody><tfoot><tr><th style=\"width:26px\"></th><th style=\"width:76px\"></th><th style=\"width:160px\"></th><th></th><th style=\"width:60px\"></th><th></th><th style=\"width:60px\"></th><th style=\"width:80px\"></th><th style=\"width:60px\"></th><th style=\"width:80px\"></th></tr></tfoot></table></div>");
                    $("#dvDialog").append(buf.join(""));

                    MISSY.iShowDialog("dvDialog", "条块之间工作职责人员对接明细表");

                    var viewHeight = $("#dvDialog").height();
                    var fixHeight = $("#" + objId + " table.tb_fix").height();
                    var areaHeight = $("#" + objId + " .content_area").height();
                    $("#" + objId + " .content_area").height(viewHeight - fixHeight - 10);
                    $("#" + objId).css("padding-top", fixHeight + "px");
                    if (areaHeight > viewHeight - fixHeight - 10) {
                        $("#" + objId + " table.tb_fix,#" + objId + " table.tb_data").width($("#" + objId + "").width() - 20);
                    }
                    $("#" + objId + " table.tb_fix tfoot tr th").each(function (idx, item) {
                        var dataTh = $("#" + objId + " table.tb_data tfoot tr th:eq(" + idx + ")");
                        var width = dataTh.outerWidth();
                        dataTh.outerWidth(width)
                        $(item).outerWidth(width);
                    })
                },
                error: function (xmlHttpRequest, textStatus, errorThrown) {
                    MISSY.iDebugAjaxError(xmlHttpRequest, textStatus, errorThrown);
                    MISSY.iErrorMessage("数据加载失败,请稍后再试.");
                },
                complete: function (xmlHttpRequest, textStatus) {
                    MISSY.iHideLoading(layerLoadIdx);
                }
            });
        },
        ShowKSDialogInfo: function (searchCode, DeptCode) {
            $("#dvDialog").children().appendTo($("#dataArea"));
            var objId = "tb_KS_" + searchCode + "_" + DeptCode;
            var tb = $("#" + objId);
            if (!tb || !tb.length) {
                var layerLoadIdx;
                $.ajax({
                    url: '/Index/Home/LoadKSInfoJson',
                    type: 'POST',
                    data: { Dept: DeptCode, InsCode: searchCode },
                    dataType: "json",
                    beforeSend: function () { layerLoadIdx = MISSY.iShowLoading("加载中，请稍候...") },
                    ContentType: "application/json;charset=utf-8",
                    success: function (response) {
                        if (!response) { MISSY.iErrorReturnNull(); }
                        switch (response.ErrorType)//标记
                        {
                            case 0://错误
                                MISSY.iErrorMessage(response.MessageContent);
                                return gridData;
                            case 1://返回正确数据
                                break;
                            case 3://未登录
                                MISSY.iNoLogin(response.MessageContent);
                                return;
                            default:
                                MISSY.iErrorMessage(response.MessageContent);
                                return gridData;
                        }
                        var data = response.Data || [];
                        var buf = ["<div id=\"" + objId + "\" class=\"table_area\"><table class=\"tb_fix\"><thead><tr><th rowspan=\"2\">序号</th><th colspan=\"2\">工作职责</th><th rowspan=\"2\">业务指导部门</th><th rowspan=\"2\">分管领导</th><th rowspan=\"2\">内设机构</th><th rowspan=\"2\">内设机构<br />负责人</th><th rowspan=\"2\">联系电话</th><th rowspan=\"2\">责任人<br />AB角</th><th rowspan=\"2\">联系电话</th></tr><tr><th>项目名称</th><th>具体内容</th></tr></thead><tfoot><tr><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th><th></th></tr></tfoot></table><div class=\"content_area\"><table class=\"tb_data\"><tbody>"];
                        var rowIdx = 0;
                        $(data).each(function (idx1, project) {
                            rowIdx++;
                            var rowCount = 1;
                            if (project.Contents && project.Contents.length && project.Contents[0].DeptCount) {
                                rowCount = project.Contents[0].DeptCount;
                            }
                            buf.push("<tr><td rowspan=\"" + rowCount + "\">" + rowIdx + "</td>");
                            buf.push("<td rowspan=\"" + project.ContentCount + "\">" + project.Project + "</td>");
                            if (project.ContentCount) {
                                $(project.Contents).each(function (idx2, content) {
                                    if (idx2 > 0) {
                                        rowIdx++;
                                        buf.push("</tr><tr><td rowspan=\"" + content.DeptCount + "\">" + rowIdx + "</td>");
                                    }
                                    buf.push("<td rowspan=\"" + content.DeptCount + "\" style=\"text-align: left;\">" + content.Content + "</td>");
                                    if (content.DeptContents && content.DeptContents.length) {
                                        $(content.DeptContents).each(function (idx3, item) {
                                            if (idx3 > 0) {
                                                buf.push("</tr><tr>");
                                            }
                                            buf.push("<td>" + (item.Dept || "") + "</td>");
                                            buf.push("<td>" + (item.Leader || "") + "</td>");
                                            buf.push("<td>" + (item.InsName || "") + "</td>");
                                            buf.push("<td>" + (item.Assistant || "") + "</td>");
                                            buf.push("<td>" + (item.AssistantPhone || "") + "</td>");
                                            buf.push("<td>" + (item.People || "") + "</td>");
                                            buf.push("<td>" + (item.PeoplePhone || "") + "</td>");
                                        })
                                    } else//没有部门对接人员时
                                        buf.push("<td></td><td></td><td></td><td></td><td></td><td></td><td></td>");
                                })
                            } else//没有具体内容时
                                buf.push("<td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td>");
                            buf.push("</tr>");
                        })
                        buf.push("</tbody><tfoot><tr><th style=\"width:30px\"></th><th style=\"width:120px\"></th><th></th><th style=\"width:150px\"></th><th style=\"width:70px\"></th><th></th><th style=\"width:70px\"></th><th style=\"width:110px\"></th><th style=\"width:70px\"></th><th style=\"width:110px\"></th></tr></tfoot></table></div>");
                        $("#dvDialog").append(buf.join(""));

                        MISSY.iShowDialog("dvDialog", "条块之间工作职责人员对接明细表");

                        var viewHeight = $("#dvDialog").height();
                        var fixHeight = $("#" + objId + " table.tb_fix").height();
                        var areaHeight = $("#" + objId + " .content_area").height();
                        $("#" + objId + " .content_area").height(viewHeight - fixHeight - 10);
                        $("#" + objId).css("padding-top", fixHeight + "px");
                        if (areaHeight > viewHeight - fixHeight - 10) {
                            $("#" + objId + " table.tb_fix,#" + objId + " table.tb_data").width($("#" + objId + "").width() - 20);
                        }
                        $("#" + objId + " table.tb_fix tfoot tr th").each(function (idx, item) {
                            var dataTh = $("#" + objId + " table.tb_data tfoot tr th:eq(" + idx + ")");
                            var width = dataTh.outerWidth();
                            dataTh.outerWidth(width)
                            $(item).outerWidth(width);
                        })
                    },
                    error: function (xmlHttpRequest, textStatus, errorThrown) {
                        MISSY.iDebugAjaxError(xmlHttpRequest, textStatus, errorThrown);
                        MISSY.iErrorMessage("数据加载失败,请稍后再试.");
                    },
                    complete: function (xmlHttpRequest, textStatus) {
                        MISSY.iHideLoading(layerLoadIdx);
                    }
                });
            } else {
                tb.appendTo($("#dvDialog"));
                MISSY.iShowDialog("dvDialog", "条块之间工作职责人员对接明细表");
            }
        }
    };
    //for 页面模块加载、Node.js运用、页面普通应用
    "function" === typeof define ? define(function () {
        return ParentHelper;
    }) : "undefined" != typeof exports ? module.exports = ParentHelper : window.ParentHelper = ParentHelper;
}(window);
