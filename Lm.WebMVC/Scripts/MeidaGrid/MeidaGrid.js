/*
 MeidaGrid
by 舒广 QQ:120782516

用法：$("#gv_list").MeidaGrid({ loadFor:"self" });

参数解释：
loadFor:呈现方式，备选值：'self':自身;'parent':父级;'window':整个可视窗口。默认为window。
mode:呈现模式，默认为true。

样式：必须设定元素的position样式为absolute，并根据需要调整z-index。
*/
(function ($) {
    $.extend(Array.prototype, {
        //从数组中获取指定元素的索引
        indexOf: function (o) {
            for (var i = 0, len = this.length; i < len; i++) {
                if (this[i] == o) {
                    return i;
                }
            }
            return -1;
        },
        //从数组中移除指定索引的元素
        remove: function (o) {
            if (typeof this.indexOf !== "function")
                return this;
            var index = this.indexOf(o);
            if (index != -1) {
                this.splice(index, 1);
            }
            return this;
        },
        //从数组中移除指定键值的元素
        removeById: function (filed, id) {
            for (var i = 0, len = this.length; i < len; i++) {
                if (this[i][filed] == id) {
                    this.splice(i, 1);
                    return this;
                }
            }
            return this;
        }
    });
    //控件主体
    $.fn.MeidaGrid = function (options, param) {
        if (typeof options == "string") {
            return $.fn.MeidaGrid.methods[options](this, param);
        }
        options = options || {};
        return this.each(function () {
            var grid = $.data(this, "meidagrid");
            var opts;
            if (grid) {
                opts = $.extend(grid.options, options);
                grid.options = opts;
            } else {
                opts = $.extend({}, $.fn.MeidaGrid.defaults, $.fn.MeidaGrid.parseOptions(this), options);

                $.data(this, "meidagrid", { options: opts, pager: {}, columnSize: [], selectedRows: [], data: { total: 0, rows: [] } });

                initDatagrid(this);
                if (opts.url) {
                    request(this);
                }
            }
        });
    }
    //控件方法
    $.fn.MeidaGrid.methods = {
        options: function (jq) {
            var opts = $.data(jq[0], "meidagrid").options;
            return opts;
        },
        pager: function (jq) {
            var pager = $.data(jq[0], "meidagrid").pager;
            return pager;
        },
        load: function (jq, param) {
            return jq.each(function () {
                var pager = $.data(jq[0], "meidagrid").pager;
                pager.pageNumber = 1;
                request(this, param);
            });
        },
        reload: function (jq, param) {
            return jq.each(function () {
                request(this, param);
            });
        },
        loading: function (jq) {
            return jq.each(function () {
                var opts = $.data(this, "meidagrid").options;
                var loadMsg = opts.loadMsg || $.fn.MeidaGrid.defaults.loadMsg;
                var columnsCount = opts.columns && opts.columns.length || $(this).find("tr:eq(0) th").length;
                $(this).find("tr.grid_header th:eq(0) :checkbox").prop("checked", false);
                $(this).find("tr:not(.grid_header)").remove();
                $(this).append("<tr class=\"loading\"><td colspan=\"" + columnsCount + "\">" + loadMsg + "</td></tr>");
                $(this).siblings(".Meidagrid-pager").find(".Meidagrid-pager-button").addClass("Meidagrid-pager-button-disabled");
            });
        },
        loadData: function (jq, data) {
            return jq.each(function () {
                loadData(this, data);
            });
        },
        getData: function (jq) {
            return $.data(jq[0], "meidagrid").data;
        },
        getRows: function (jq) {
            return $.data(jq[0], "meidagrid").data.rows;
        },
        getRowIndex: function (jq, id) {
            return getRowIndex(jq[0], id);
        },
        getSelected: function (jq) {
            var rows = getSelectRows(jq[0]);
            return rows.length > 0 ? rows[0] : null;
        },
        getSelections: function (jq) {
            return getSelectRows(jq[0]);
        },
        getSelectionIDFields: function (jq) {
            var idArry = [];
            var opts = $.data(jq[0], "meidagrid").options;
            if (opts.idField) {
                var rows = $.data(jq[0], "meidagrid").selectedRows;
                $(rows).each(function () {
                    idArry.push(this[opts.idField]);
                });
            }
            return idArry.join(",");
        },
        clearSelections: function (jq) {
            return jq.each(function () {
                clearSelections(this);
            });
        },
        selectAll: function (jq) {
            return jq.each(function () {
                selectAll(this);
            });
        },
        unselectAll: function (jq) {
            return jq.each(function () {
                clearSelectRows(this);
            });
        },
        selectRow: function (jq, index) {
            return jq.each(function () {
                selectRow(this, index);
            });
        },
        selectRecord: function (jq, id) {
            return jq.each(function () {
                selectRecord(this, id);
            });
        },
        unselectRow: function (jq, index) {
            return jq.each(function () {
                unSelectRow(this, index);
            });
        }
    };
    //默认属性
    $.fn.MeidaGrid.defaults = $.extend({}, {
        width: null,
        columns: null,
        striped: false,
        method: "post",
        idField: null,
        url: null,
        loadMsg: "数据加载中...",
        singleSelect: false,
        pagination: false,
        pageNumber: 1,
        pageSize: 10,
        queryParams: {},
        sorts: [],
        rowStyler: function (rowIndex, rowData) { },
        loadFilter: function (data) {
            if (typeof data.length == "number" && typeof data.splice == "function") {
                return { total: data.length, rows: data };
            } else {
                return data;
            }
        },
        onBeforeLoad: function (param) { },
        onLoadSuccess: function () { },
        onLoadError: function () { },
        onRenderComplete: function () { },
        onClickRow: function (rowIndex, rowData) { },
        onSelect: function (rowIndex, rowData) { },
        onUnselect: function (rowIndex, rowData) { }
    });
    $.fn.MeidaGrid.parseOptions = function (target) {
        var t = $(target);
        return $.extend({}, parseOptions(target, ["id", "width"]));
    }
    //公用的属性转换器，兼容data-options方式和老方式，功能可谓灵活而强大
    function parseOptions(target, parseOptions) {
        var t = $(target);

        var opts = {};
        var data_options = $.trim(t.attr("data-options"));

        if (data_options) {
            var first_str = data_options.substring(0, 1);

            var last_str = data_options.substring(data_options.length - 1, 1);
            if (first_str != "{") {
                data_options = "{" + data_options;
            }
            if (last_str != "}") {
                data_options = data_options + "}";
            }

            opts = (new Function("return " + data_options))();
            //console.info(opts);
        }

        if (parseOptions) {
            var _b = {};
            for (var i = 0; i < parseOptions.length; i++) {
                var pp = parseOptions[i];
                if (typeof pp == "string") {
                    if (pp == "width" || pp == "height" || pp == "left" || pp == "top") {
                        _b[pp] = parseInt(target.style[pp]) || undefined;
                    } else {
                        _b[pp] = t.attr(pp);
                    }
                } else {
                    for (var _c in pp) {
                        var _d = pp[_c];
                        if (_d == "boolean") {
                            _b[_c] = t.attr(_c) ? (t.attr(_c) == "true") : undefined;
                        } else {
                            if (_d == "number") {
                                _b[_c] = t.attr(_c) == "0" ? 0 : parseFloat(t.attr(_c)) || undefined;
                            }
                        }
                    }
                }
            }
            $.extend(opts, _b);
        }
        return opts;
    }
    //私有方法
    function initDatagrid(target) {
        var opts = $.data(target, "meidagrid").options;
        var pager = $.data(target, "meidagrid").pager;

        var element = $(target);//控件对象
        element.css("width", "100%");
        if (!element.hasClass("MeidaGrid")) element.addClass("MeidaGrid");
        if (!element.parent().hasClass("MeidaGridArea")) element.wrap("<div class=\"MeidaGridArea\"></div>");
        var gridArea = element.parent(".MeidaGridArea");
        //宽度
        if (undefined !== opts.width) {
            (typeof opts.width === "number" && gridArea.width(opts.width)) || (typeof opts.width === "string" && gridArea.css("width", opts.width))
        }
        var columnsCount = 1;
        /*表头 Begin*/
        var trHeadObj = $("<tr class=\"grid_header\"></tr>");
        if (undefined !== opts.columns && opts.columns.length > 0) {
            columnsCount = opts.columns.length;
            $(opts.columns).each(function () {
                var column = this;
                var thObj = $("<th></th>").appendTo(trHeadObj);
                //宽度
                if (undefined !== column.width) {
                    (typeof column.width === "number" && thObj.width(column.width)) || (typeof column.width === "string" && thObj.css("width", column.width))
                }
                //复选框（全选/全不选）
                if (undefined !== column.checkbox && true === column.checkbox) {
                    if (true !== opts.singleSelect) {
                        thObj.addClass("chkAll");
                        var chkObj = $("<input type=\"checkbox\" />").appendTo(thObj);
                        chkObj.on("click", function () {
                            if (this.checked) selectAll(target)
                            else clearSelectRows(target);
                        })
                    }
                }
                //标题与排序
                var caption = undefined !== column.title && column.title || "";
                if (undefined !== column.Sort && false !== column.Sort) {
                    thObj.addClass("sort");
                    var sortObj = $("<span>" + caption + "<i></i></span>").appendTo(thObj);
                    sortObj.on("click", function () {
                        if (element.find("tr.loading").length > 0) return;
                        var parent = sortObj.parent();
                        if (parent.hasClass("desc")) {
                            opts.sorts.removeById("field", column.field);
                            parent.removeClass("desc");
                        }
                        else if (parent.hasClass("asc")) {
                            for (var s = 0, m = opts.sorts.length; s < m; s++) {
                                if (opts.sorts[s].field === column.field) {
                                    opts.sorts[s].sort = "desc";
                                }
                            }
                            parent.removeClass("asc").addClass("desc");
                        }
                        else {
                            opts.sorts.push({ field: column.field, sort: "asc" });
                            parent.addClass("asc");
                        }
                        request(target);
                    });
                    if ("asc" === column.Sort) {
                        opts.sorts.push({ field: column.field, sort: column.Sort });
                        thObj.addClass("asc");
                    } else if ("desc" === column.Sort) {
                        opts.sorts.push({ field: column.field, sort: column.Sort });
                        thObj.addClass("desc");
                    }
                } else {
                    thObj.append(caption);
                }
            })
            element.append(trHeadObj);
        }
        var sizeArray = [];
        var header = element.find("tr.grid_header");
        if (header.length === 1) {
            $(header).contents().each(function () { sizeArray.push($(this).width()); });
        } else {

        }
        $.data(target, "meidagrid").columnSize = sizeArray;
        /*表头 End*/
        /*空数据行 Begin*/
        var trEmptyObj = $("<tr class=\"empty\"><td colspan=\"" + columnsCount + "\">没有可供展示的数据</td></tr>");
        element.append(trEmptyObj);
        /*空数据行 End*/
        /*分页工具条 Begin*/
        if (undefined !== opts.pagination && true === opts.pagination) {
            $.data(target, "meidagrid").pager = $.extend({}, pager, { pageNumber: opts.pageNumber, pageSize: opts.pageSize, pageCount: 0, totalRecord: 0 });
            var pagerEle = $("<div class=\"Meidagrid-pager\"><div class=\"Meidagrid-pager-left\"><a class=\"Meidagrid-pager-button Meidagrid-pager-button-disabled pager-first\"></a><a class=\"Meidagrid-pager-button Meidagrid-pager-button-disabled pager-prev\"></a><label class=\"Meidagrid-pager-num\"><input type=\"text\" class=\"pager-go\" value=\"1\" />/<span class=\"pager-Count\">0</span></label><a class=\"Meidagrid-pager-button Meidagrid-pager-button-disabled pager-next\"></a><a class=\"Meidagrid-pager-button Meidagrid-pager-button-disabled pager-last\"></a><span class=\"separator\"></span><a class=\"Meidagrid-pager-button-reload pager-reload\"></a></div><div class=\"Meidagrid-pager-right\">第<span class=\"pager-index\">1</span>页/共<span class=\"pager-Count\">0</span>页, 每页<span class=\"pager-size\">10</span>条, 共<span class=\"pager-total\">0</span>条</div></div>").appendTo(gridArea);

            pagerEle.find(".pager-go").on("keyup", function (event) {
                if (element.find("tr.loading").length > 0) return;
                var e = event ? event : window.event;
                var keycode = (e.which ? e.which : e.keyCode);
                var tempGoPage = $(this).val();
                if (tempGoPage != null) {
                    if (!tempGoPage.match(/^[1-9]+\d*$/)) {
                        $(this).val("")
                        return;
                    }
                }
                if (keycode === 13) {
                    var goPage = parseInt(tempGoPage);

                    var selectRows = $.data(target, "meidagrid").selectedRows;
                    if (selectRows && selectRows.length)
                        clearSelections(target);

                    var pager = $.data(target, "meidagrid").pager;
                    pager.pageNumber = goPage > pager.pageCount && pager.pageCount || goPage;
                    $.data(target, "meidagrid").pager = pager;
                    request(target);
                }
            });
            pagerEle.find(".pager-first").on("click", function () {
                if ($(this).hasClass("Meidagrid-pager-button-disabled")) return;
                var selectRows = $.data(target, "meidagrid").selectedRows;
                if (selectRows && selectRows.length)
                    clearSelections(target);

                var pager = $.data(target, "meidagrid").pager;
                pager.pageNumber = 1;
                $.data(target, "meidagrid").pager = pager;

                request(target);
            });
            pagerEle.find(".pager-prev").on("click", function () {
                if ($(this).hasClass("Meidagrid-pager-button-disabled")) return;

                var selectRows = $.data(target, "meidagrid").selectedRows;
                if (selectRows && selectRows.length)
                    clearSelections(target);

                var pager = $.data(target, "meidagrid").pager;
                var pagerNow = pager.pageNumber;
                if (typeof pagerNow !== "number")
                    pagerNow = isNaN(pagerNow) ? 2 : parseInt(pagerNow);
                pager.pageNumber = pagerNow - 1;
                $.data(target, "meidagrid").pager = pager;
                request(target);
            });
            pagerEle.find(".pager-next").on("click", function () {
                if ($(this).hasClass("Meidagrid-pager-button-disabled")) return;

                var selectRows = $.data(target, "meidagrid").selectedRows;
                if (selectRows && selectRows.length)
                    clearSelections(target);

                var pager = $.data(target, "meidagrid").pager;
                var pagerNow = pager.pageNumber;
                if (typeof pagerNow !== "number")
                    pagerNow = isNaN(pagerNow) ? pager.pageCount : parseInt(pagerNow);
                pager.pageNumber = pagerNow + 1;
                $.data(target, "meidagrid").pager = pager;
                request(target);
            });
            pagerEle.find(".pager-last").on("click", function () {
                if ($(this).hasClass("Meidagrid-pager-button-disabled")) return;

                var selectRows = $.data(target, "meidagrid").selectedRows;
                if (selectRows && selectRows.length)
                    clearSelections(target);

                var pager = $.data(target, "meidagrid").pager;
                pager.pageNumber = pager.pageCount
                $.data(target, "meidagrid").pager = pager;
                request(target);
            });
            pagerEle.find(".pager-reload").on("click", function () {
                request(target);
            });

        }
        /*分页工具条 End*/
    };
    function request(target, param) {
        var opts = $.data(target, "meidagrid").options;
        var pager = $.data(target, "meidagrid").pager;
        if (param) {
            opts.queryParams = param;
        }
        if (!opts.url) {
            return;
        }
        var queryObj = $.extend({}, opts.queryParams);
        if (opts.pagination) {
            if (pager)
                $.extend(queryObj, { page: pager.pageNumber, size: pager.pageSize });
            else
                $.extend(queryObj, { page: opts.pageNumber, size: opts.pageSize });
        }
        if (opts.sorts.length) {
            var sortA = [];
            for (var i = 0, m = opts.sorts.length; i < m; i++) {
                sortA.push(opts.sorts[i].field + "|" + opts.sorts[i].sort);
            }
            $.extend(queryObj, { sort: sortA.join(",") });
        }
        if (opts.onBeforeLoad.call(target, queryObj) == false) {
            return;
        }
        $(target).MeidaGrid("loading");
        setTimeout(function () {
            ajaxRequest();
        }, 0);
        function ajaxRequest() {
            $.ajax({
                type: opts.method,
                url: opts.url,
                data: queryObj,
                dataType: "json",
                success: function (data) {
                    loadData(target, data);
                },
                error: function () {
                    if (opts.onLoadError) {
                        opts.onLoadError.apply(target, arguments);
                    }
                }
            });
        };
    };
    function loadData(target, data) {
        var opts = $.data(target, "meidagrid").options;
        var pager = $.data(target, "meidagrid").pager;
        var columnSize = $.data(target, "meidagrid").columnSize;
        var selectRows = $.data(target, "meidagrid").selectedRows;
        var element = $(target);
        var pagerEle = element.siblings("div.Meidagrid-pager");

        opts.onLoadSuccess.call(target, data);

        data = data || {};
        data = opts.loadFilter.call(target, data);
        $.data(target, "meidagrid").data = data;

        var dataRows = data.rows || [];
        var rowIdx = pagerEle.length && pager.pageNumber * pager.pageSize - pager.pageSize + 1 || 1;
        var buf = [];
        var striped = opts.striped;
        for (var i = 0, mR = dataRows.length; i < mR; i++) {
            var model = dataRows[i];
            var rowStyle = opts.rowStyler ? opts.rowStyler.call(target, i, model) : "";
            var strStyle = rowStyle ? "style=\"" + rowStyle + "\"" : "";
            buf.push("<tr class=\"rows-index-" + i + (striped && " strip_bg" + i % 2 || "") + "\" " + strStyle + " row-index=\"" + i + "\">");
            if (opts.columns) {
                for (var j = 0, mC = opts.columns.length; j < mC; j++) {
                    var item = opts.columns[j];
                    //复选框
                    if (undefined !== item.checkbox && true === item.checkbox) {
                        var chkStr = "<input type=\"checkbox\" class=\"multiple-chk\"/>";
                        //列内容格式化器
                        if (undefined !== item.formatter) {
                            if (typeof item.formatter === "function")
                                chkStr = item.formatter(model, i);
                        }
                        buf.push("<td><label class=\"col" + j + "\">" + chkStr + "</label></td>");
                    } else if (undefined !== item.rowNum && true === item.rowNum) {
                        //行号
                        buf.push("<td><label class=\"col" + j + "\">" + (rowIdx + i) + "</label></td>");
                    } else {
                        //数据列
                        var captionStr = "", classStr = "col" + j;
                        if (undefined !== item.align) {
                            switch (item.align) {
                                case "left": classStr += " algin_l"; break;
                                case "right": classStr += " algin_r"; break;
                                case "center":
                                case "auto":
                                default: classStr += " algin_c"; break;
                            }
                        }
                        //列内容
                        if (undefined !== item.field && undefined !== model[item.field] && null !== model[item.field]) {
                            captionStr = model[item.field];
                        }
                        //列内容格式化器
                        if (undefined !== item.formatter) {
                            if (typeof item.formatter === "function")
                                captionStr = item.formatter(captionStr, model, i);
                        }
                        //操作列
                        if (undefined !== item.operate && true === item.operate) {
                            classStr += " operate";
                        }
                        buf.push("<td><label class=\"" + classStr + "\">" + captionStr + "</label></td>");
                    }
                }
            }
            buf.push("</tr>")
        }
        element.find("tr:not(.grid_header)").remove();
        if (buf.length === 0) {
            var columnsCount = opts.columns && opts.columns.length || element.find("tr:eq(0) th").length;
            element.append("<tr class=\"empty\"><td colspan=\"" + columnsCount + "\">没有可供展示的数据</td></tr>");
        }
        else {
            element.append(buf.join(""));
            $(columnSize).each(function (idx, value) {
                element.find("tr:not(.grid_header) td label.col" + idx).outerWidth(value).each(function () {
                    if (!$(this).hasClass("operate"))
                        $(this).attr("title", $(this).text());
                });
            });
            if (opts.idField && selectRows.length) {
                for (var i = 0, mD = dataRows.length; i < mD; i++) {
                    var row = data.rows[i];
                    for (var j = 0, mS = selectRows.length; j < mS; j++) {
                        if (selectRows[j][opts.idField] == row[opts.idField]) {
                            element.find("tr.rows-index-" + i).addClass("selected");
                            element.find("tr.rows-index-" + i + " td:eq(0) input[type=checkbox]").prop("checked", true);
                        }
                    }
                }
            }
        }
        if (pagerEle.length > 0) {
            var pageCount = 0;
            if (data.total % pager.pageSize === 0) {
                pageCount = data.total / pager.pageSize;
            } else {
                pageCount = parseInt(data.total / pager.pageSize) + 1;
            }
            pager.total = data.total;
            pager.pageCount = pageCount;
            $.data(target, "meidagrid").pager = pager;

            if (pageCount > 1) {
                if (pager.pageNumber > 1) {
                    pagerEle.find(".pager-first").removeClass("Meidagrid-pager-button-disabled");
                    pagerEle.find(".pager-prev").removeClass("Meidagrid-pager-button-disabled");
                }
                if (pager.pageNumber < pageCount) {
                    pagerEle.find(".pager-next").removeClass("Meidagrid-pager-button-disabled");
                    pagerEle.find(".pager-last").removeClass("Meidagrid-pager-button-disabled");
                }
            }

            pagerEle.find(".pager-go").val(pager.pageNumber);
            pagerEle.find(".pager-index").text(pager.pageNumber);
            pagerEle.find(".pager-Count").text(pager.pageCount);
            pagerEle.find(".pager-size").text(pager.pageSize);
            pagerEle.find(".pager-total").text(pager.total);
        }
        if (typeof opts.onRenderComplete === "function") {
            opts.onRenderComplete(target, arguments);
        }
        bindRowEvents(target);

    };
    function selectRecord(target, id) {
        var opts = $.data(target, "meidagrid").options;
        var data = $.data(target, "meidagrid").data;
        if (opts.idField) {
            var idx = -1;
            for (var i = 0, m = data.rows.length; i < m; i++) {
                if (data.rows[i][opts.idField] == id) {
                    idx = i;
                    break;
                }
            }
            if (idx >= 0) {
                selectRow(target, idx);
            }
        }
    };
    function selectRow(target, index) {
        var opts = $.data(target, "meidagrid").options;
        var data = $.data(target, "meidagrid").data;
        var selectRows = $.data(target, "meidagrid").selectedRows;
        if (index < 0 || index >= data.rows.length) {
            return;
        }
        if (opts.singleSelect == true) {
            clearSelections(target);
        }
        var tr = $("tr.rows-index-" + index, target);
        if (!tr.hasClass("selected")) {
            tr.addClass("selected");
            var ck = $("td:eq(0) :checkbox.multiple-chk", tr);
            ck.prop("checked", true);
            if (opts.idField) {
                var row = data.rows[index];
                for (var i = 0, m = selectRows.length; i < m; i++) {
                    if (selectRows[i][opts.idField] == row[opts.idField]) {
                        return;
                    }
                }
                selectRows.push(row);
            }
        }
        opts.onSelect.call(target, index, data.rows[index]);
    };
    function unSelectRow(target, index) {
        var opts = $.data(target, "meidagrid").options;
        var data = $.data(target, "meidagrid").data;
        var selectRows = $.data(target, "meidagrid").selectedRows;
        if (index < 0 || index >= data.rows.length) {
            return;
        }
        var tr = $("tr.rows-index-" + index, target);
        var ck = $("td:eq(0) :checkbox.multiple-chk", tr);
        tr.removeClass("selected");
        ck.prop("checked", false);
        var row = data.rows[index];
        if (opts.idField) {
            selectRows.removeById(opts.idField, row[opts.idField]);
        }
        opts.onUnselect.call(target, index, row);
    };
    function getRowIndex(target, row) {
        var opts = $.data(target, "meidagrid").options;
        var rows = $.data(target, "meidagrid").data.rows;
        if (typeof row == "object") {
            return rows.indexOf(row);
        } else {
            for (var i = 0; i < rows.length; i++) {
                if (rows[i][opts.idField] == row) {
                    return i;
                }
            }
            return -1;
        }
    };
    function getSelectRows(target) {
        var opts = $.data(target, "meidagrid").options;
        var data = $.data(target, "meidagrid").data;
        if (opts.idField) {
            return $.data(target, "meidagrid").selectedRows;
        } else {
            var rowAry = [];
            $("tr.selected", target).each(function () {
                var row_index = parseInt($(this).attr("row-index"));
                rowAry.push(data.rows[row_index]);
            });
            return rowAry;
        }
    };
    function clearSelections(target) {
        clearSelectRows(target);
        var selectRows = $.data(target, "meidagrid").selectedRows;
        selectRows.splice(0, selectRows.length);
    };
    function selectAll(target) {
        var opts = $.data(target, "meidagrid").options;
        var data = $.data(target, "meidagrid").data;
        var selectRows = $.data(target, "meidagrid").selectedRows;
        var rows = data.rows;
        $("tr:not(.grid_header,.loading,.empty,.error)", target).addClass("selected");
        $("tr:not(.grid_header)", target).each(function () {
            $(this).find("td:eq(0) :checkbox").prop("checked", true);
        });
        if (opts.idField) {
            for (var i = 0, mi = rows.length; i < mi; i++) {
                var f = true;
                for (var j = 0, mj = selectRows.length; j < mj; j++) {
                    if (selectRows[j][opts.idField] == rows[i][opts.idField]) {
                        f = false;
                        break;
                    }
                }
                if (f) selectRows.push(rows[i]);
            }
        }
    };
    function clearSelectRows(target) {
        var opts = $.data(target, "meidagrid").options;
        var data = $.data(target, "meidagrid").data;
        var selectRows = $.data(target, "meidagrid").selectedRows;
        $("tr.selected", target).removeClass("selected");
        $("tr:not(.grid_header)", target).each(function () {
            $(this).find("td:eq(0) :checkbox").prop("checked", false);
        });
        if (opts.idField && data.rows) {
            for (var i = 0; i < data.rows.length; i++) {
                selectRows.removeById(opts.idField, data.rows[i][opts.idField]);
            }
        }
    };
    function bindRowEvents(target) {
        var opts = $.data(target, "meidagrid").options;
        var data = $.data(target, "meidagrid").data;
        $(target).find("tr[row-index]").bind("click", function () {
            var findIndex = $(this).attr("row-index");
            if (opts.singleSelect == true) {
                clearSelections(target);
                selectRow(target, findIndex);
            } else {
                if ($(this).hasClass("selected")) {
                    unSelectRow(target, findIndex);
                } else {
                    selectRow(target, findIndex);
                }
            }
            if (opts.onClickRow) {
                opts.onClickRow.call(target, findIndex, data.rows[findIndex]);
            }
        })
    };
    function newGuid() {
        return 'xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx'.replace(/x/g, function (c) {
            return Math.floor(Math.random() * 16.0).toString(16);
        });
    }
})(jQuery);
