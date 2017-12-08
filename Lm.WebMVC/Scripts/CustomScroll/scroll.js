var get = {
    byId: function (id) {
        return typeof id === "string" ? document.getElementById(id) : id;
    },
    sty: function (obj, name) {
        return obj.currentStyle ? obj.currentStyle[name] : getComputedStyle(obj, null)[name];
    },
    obx_H: function (obj) {
        var obj = obj.getBoundingClientRect();
        return obj.bottom - obj.top;
    },
    ScoTop: function () {
        return document.documentElement.scrollTop || document.body.scrollTop;
    }
};

function enclose(con_Ov, con_Ov_H, con, con_H, scool, scool_H) {
    var isFirefox = (navigator.userAgent.indexOf("Gecko") !== -1);
    if (document.onwheel !== undefined) {
        con_Ov.onwheel = wheelHandler;
    } else if (document.onmousewheel !== undefined) {
        con_Ov.onmousewheel = wheelHandler;
    } else if (isFirefox) {
        con_Ov.addEventListener("DOMMouseScroll", wheelHandler, false);
    }

    function wheelHandler(e) {
        var e = e || window.event,
          deltaY = e.wheelDeltaY || (e.wheelDeltaY === undefined && e.wheelDelta) || e.deltaY * -1 || e.detail * -1 || 0,
          con_Top,// = parseFloat(get.sty(con,"top")),
          con_MaxH = con_H - con_Ov_H, //内容的最大滚动距离
          scool_MaxH = con_Ov_H - scool_H,//左侧最大滚动距离
          gd_Auto,//滚动设置的Interval对象
          star_Time = 0, //滚动开始时间
          now_Time = 0,//滚动结束时间
          step = 10; //每次滚动多少像素
        if (gd_Auto) { clearInterval(gd_Auto); }
        star_Time = new Date().getTime();
        if (deltaY < 0) {
            gd_Auto = setInterval(function () {
                con_Top = parseFloat(get.sty(con, "top")),
                  now_Time = new Date().getTime();

                if (con_Top <= -con_MaxH) {
                    clearInterval(gd_Auto);
                    con.style.top = -con_MaxH + "px";
                    scool.style.top = scool_MaxH + "px";
                    return;
                }

                con.style.top = con_Top - step + "px";
                scool.style.top = Math.round(-(con_Top - step) / con_MaxH * scool_MaxH) + "px";

                if (con_Top - step <= -con_MaxH || now_Time - star_Time > 100) {
                    clearInterval(gd_Auto);
                    con_Top - step <= -con_MaxH && (con.style.top = -con_MaxH + "px", scool.style.top = scool_MaxH + "px");
                    return;
                }
            }, 20)

        } else if (deltaY > 0) {
            gd_Auto = setInterval(function () {
                con_Top = parseFloat(get.sty(con, "top")),
                  now_Time = new Date().getTime();
                if (con_Top >= 0) {
                    clearInterval(gd_Auto);
                    con.style.top = 0 + "px";
                    scool.style.top = 0 + "px";
                    return;
                }

                con.style.top = con_Top + step + "px";
                scool.style.top = Math.round(-(con_Top + step) / con_MaxH * scool_MaxH) + "px";

                if (con_Top + step >= 0 || now_Time - star_Time > 200) {
                    clearInterval(gd_Auto);
                    con_Top + step >= 0 && (con.style.top = 0 + "px", scool.style.top = 0 + "px");
                    return;
                }
            }, 20)
        } else {
            alert("错误！");
        }

        if (e.stopPropagation) e.stopPropagation();
        else e.cancelBubble = true;
        if (e.preventDefault) e.preventDefault();
        else e.returnValue = false;
    }
}

function drag(con_Ov, con_Ov_H, con, con_H, ele, ele_H, e) {
    var e = e || window.event,
      scroll_T = get.ScoTop(),
      sc_H = con_Ov_H - ele_H,
      startY = e.clientY + scroll_T,
      origY = ele.offsetTop,
      deltaY = startY - origY;

    if (document.addEventListener) {
        document.addEventListener("mousemove", moveHandler, true);
        document.addEventListener("mouseup", upHandler, true);
    } else if (document.attachEvent) {  // IE Event Model for IE5-8
        ele.setCapture();
        ele.attachEvent("onmousemove", moveHandler);
        ele.attachEvent("onmouseup", upHandler);
        ele.attachEvent("onlosecapture", upHandler);
    }

    if (e.stopPropagation) e.stopPropagation();
    else e.cancelBubble = true;
    if (e.preventDefault) e.preventDefault();
    else e.returnValue = false;

    function moveHandler(e) {
        var e = e || window.event,
          ele_Top = Math.max(e.clientY + scroll_T - deltaY, 0) && Math.min(e.clientY + scroll_T - deltaY, sc_H),
          con_Top = -ele_Top / sc_H * (con_H - con_Ov_H);

        ele.style.top = ele_Top + "px";
        con.style.top = con_Top + "px";

        if (e.stopPropagation) e.stopPropagation();
        else e.cancelBubble = true;
    }

    function upHandler(e) {
        var e = e || window.event;
        if (document.removeEventListener) {
            document.removeEventListener("mouseup", upHandler, true);
            document.removeEventListener("mousemove", moveHandler, true);
        } else if (document.detachEvent) {
            ele.detachEvent("onlosecapture", upHandler);
            ele.detachEvent("onmouseup", upHandler);
            ele.detachEvent("onmousemove", moveHandler);
            ele.releaseCapture();
        }
        if (e.stopPropagation) e.stopPropagation();
        else e.cancelBubble = true;
    }
}

function jsScroll(idName, height, clName01, clName02) {
    var con_Ov = get.byId(idName),
      con_Ov_Height = con_Ov.clientHeight,
      cont = con_Ov.getElementsByTagName("div")[0],
      cont_H = Math.round(get.obx_H(cont));

    if (!con_Ov_Height || con_Ov_Height == "auto" || con_Ov_Height >= height) {
        con_Ov.style.height = height + "px";
        con_Ov.style.overflow = "hidden";
        con_Ov_Height = height;
    }

    if (cont_H <= con_Ov_Height) return;
    cont.style.position = "relative";//设置内容必要CSS样式；
    cont.style.top = "0px";
    cont.style.zIndex = "0";

    var scool = document.createElement("div"), scool_An = document.createElement("div");//添加滚动条
    if (clName01) {
        scool.className = clName01;
    } else {
        scool.style.cssText = "position:absolute; top:0; right:0; z-index:99; width:10px; background-color:#fff;";
    }
    scool.style.height = con_Ov_Height + "px";
    con_Ov.appendChild(scool);
    if (clName02) {
        scool_An.className = clName02;
    } else {
        scool_An.style.cssText = "position:absolute; top:0; right:0; z-index:999; width:10px; height:30px; background-color:#999;";
    }
    con_Ov.appendChild(scool_An);

    var scool_AnH = parseFloat(get.sty(scool_An, "height"));
    scool_An.onmousedown = function (e) {
        drag(con_Ov, con_Ov_Height, cont, cont_H, this, scool_AnH, e);
    }
    enclose(con_Ov, con_Ov_Height, cont, cont_H, scool_An, scool_AnH);
}
