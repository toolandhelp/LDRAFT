var WinHeight = 0,//可视高度
    MaxWidth = 0,//画布宽度
    MaxHeight = 0,//画布高度
    ContentWidth = 0,//内容宽度
    ContentHeight = 0,//内容高度
    ExceptOffset = 0,//画布偏移量
    DefaultTop = 20,//纵向间隔默认值
    DefaultLeft = 100,//横向间隔默认值
    offsetTop = 20,//纵向间隔
    offsetLeft = 0,//横向间隔
    topItemValue = 20,//纵向偏移
    leftItemValue = 0,//横向偏移
    lineColor = ["#7E2018", "#24591B", "#3F3364"],//连接线颜色
    lineSize = 2;//连接线尺寸(宽度(高度)+边框线*2)
function InitCanvasArea(root) {
    root.siblings().each(function () {
        $(this).outerWidth($(this).outerWidth());
    });
    var leaf = $("[pid='" + root.prop("id") + "']");
    if (leaf.length) {
        if (leaf.length === 1) {
            var singleArray = [];
            singleArray.push([leaf[0]]);
            BuilderArray(singleArray, [leaf[0]]);

            SetSingleOffset(root, singleArray);

            SetRightLine(root, leaf, 0);
        } else {
            var count = leaf.length,
                num = count % 2 === 0 ? count / 2 : parseInt(count / 2) + 1;
            var leftArray = [], rightArray = [], lA = [], rA = [];
            for (var i = 0; i < num; i++) {
                lA.push(leaf[i * 2]);
                if (i * 2 + 1 < count)
                    rA.push(leaf[i * 2 + 1]);
            }
            leftArray.push(lA);
            BuilderArray(leftArray, lA, 3);
            rightArray.push(rA);
            BuilderArray(rightArray, rA, 3);

            SetMoreItemOffset(root, leftArray, rightArray);

            SetLeftLine(root, $("[pid='" + root.prop("id") + "']:even"), 0);
            SetRightLine(root, $("[pid='" + root.prop("id") + "']:odd"), 0);
        }
        $(".conn_line").show();
    } else {
        root.css({ "top": ((MaxHeight - root.outerHeight()) / 2) + "px", "left": ((MaxWidth - root.outerWidth()) / 2) + "px" });
    }
}
//设置左侧连线
function SetLeftLine(item, childs, level) {
    var childCount = childs.length;//子节点数量
    if (!childCount) return;//子节点不存在时，返回
    var contentObj = item.parents("div.Area"),//画布
        ExceptOffset = contentObj.offset(),//画布偏移量
        offset = item.offset(),//父节点偏移量
        topLineValue = 0,//线的偏移量(top)
        leftLineValue = 0,//线的偏移量(left)
        width = 0,//线的宽度
        height = 0;//线的高度
    if (childCount == 1) {//单子节点时，只要设置父子节点的连线
        var lineObj = $("<div class=\"conn_line\"></div>").appendTo(contentObj);
        var child = $(childs[0]),
            childOffset = child.offset(),
            childWidth = child.outerWidth(),
            childHeight = child.outerHeight(),
            itemLeft = offset.left;
        leftLineValue = childOffset.left + childWidth - ExceptOffset.left;
        topLineValue = childOffset.top + (childHeight - lineSize) / 2 - ExceptOffset.top;
        width = itemLeft - (childOffset.left + childWidth + lineSize);
        lineObj.css({ "border-color": lineColor[level], "width": width + "px", "top": topLineValue + "px", "left": leftLineValue + "px" });
        SetLeftLine(child, $("[pid='" + child.prop("id") + "'].item"), level + 1);
    } else {//多子节点时，需要设置多设置一条纵向中间线与一条横向中间线
        for (var i = 0, max = childs.length; i < max; i++) {
            var lineObj = $("<div class=\"conn_line\"></div>").appendTo(contentObj);
            var child = $(childs[i]),
            childOffset = child.offset(),
            childWidth = child.outerWidth(),
            childHeight = child.outerHeight(),
            itemLeft = offset.left;
            var leftV = childOffset.left + childWidth - ExceptOffset.left,
                topV = childOffset.top + (childHeight - lineSize) / 2 - ExceptOffset.top,
                widthV = (itemLeft - (childOffset.left + childWidth + lineSize)) / 2;
            lineObj.css({ "border-color": lineColor[level], "width": widthV + "px", "top": topV + "px", "left": leftV + "px" });
            SetLeftLine(child, $("[pid='" + child.prop("id") + "'].item"), level + 1);
            if (i === 0) {
                topLineValue = topV;
                leftLineValue = leftV + widthV + lineSize;
            } else if (i === max - 1) {
                height = topV - topLineValue;
            }
        }
        //多子节点时，需要多一条纵向中间线连接各子节点
        var lineYObj = $("<div class=\"conn_line\"></div>").appendTo(contentObj);
        lineYObj.css({ "border-color": lineColor[level], "height": height + "px", "top": topLineValue + "px", "left": leftLineValue + "px" });

        leftLineValue += lineSize;
        topLineValue = offset.top + (item.outerHeight() - lineSize) / 2 - ExceptOffset.top;
        var width = offset.left - leftLineValue - lineSize - ExceptOffset.left;
        //多子节点时，需要多一条横向中间线连接父节点与纵向中间线
        var lineXObj = $("<div class=\"conn_line\"></div>").appendTo(contentObj);
        lineXObj.css({ "border-color": lineColor[level], "width": width + "px", "top": topLineValue + "px", "left": leftLineValue + "px" });
    }

}
//设置右侧连线
function SetRightLine(item, childs, level) {
    var childCount = childs.length;//子节点数量
    if (!childCount) return;//子节点不存在时，返回
    var contentObj = item.parents("div.Area"),//画布
        ExceptOffset = contentObj.offset(),//画布偏移量
        itemWidth = item.outerWidth(),//父节点宽度
        itemHeight = item.outerHeight(),//父节点高度
        offset = item.offset(),//父节点偏移量
        width = 0,//线的宽度
        height = 0,//线的高度
        topLineValue = offset.top + (itemHeight - lineSize) / 2 - ExceptOffset.top,//线的偏移量(top)
        leftLineValue = offset.left + itemWidth - ExceptOffset.left;//线的偏移量(left)
    if (childCount == 1) {//单子节点时，只要设置父子节点的连线
        var lineObj = $("<div class=\"conn_line\"></div>").appendTo(contentObj);
        var child = $(childs[0]),
            childLeft = child.offset().left;
        width = childLeft - (offset.left + itemWidth + lineSize);
        lineObj.css({ "border-color": lineColor[level], "width": width + "px", "top": topLineValue + "px", "left": leftLineValue + "px" });
        SetRightLine(child, $("[pid='" + child.prop("id") + "'].item"), level + 1);
    } else {//多子节点时，需要设置多设置一条纵向中间线与一条横向中间线
        var childOffset = $(childs[0]).offset(),
            width = (childOffset.left - leftLineValue - lineSize * 2 - ExceptOffset.left) / 2;
        //多子节点时，需要多一条横向中间线连接父节点与纵向中间线
        var lineXObj = $("<div class=\"conn_line\"></div>").appendTo(contentObj);
        lineXObj.css({ "border-color": lineColor[level], "width": width + "px", "top": topLineValue + "px", "left": leftLineValue + "px" });
        //
        for (var i = 0, max = childs.length; i < max; i++) {
            var lineObj = $("<div class=\"conn_line\"></div>").appendTo(contentObj);
            var child = $(childs[i]),
                childHeight = child.outerHeight(),
                childOffset = child.offset();
            var topV = childOffset.top + (childHeight - lineSize) / 2 - ExceptOffset.top,
                widthV = (childOffset.left - (offset.left + itemWidth) - lineSize * 2) / 2,
                leftV = childOffset.left - widthV - ExceptOffset.left - lineSize;
            lineObj.css({ "border-color": lineColor[level], "width": widthV + "px", "top": topV + "px", "left": leftV + "px" });
            SetRightLine(child, $("[pid='" + child.prop("id") + "'].item"), level + 1);
            if (i === 0) {
                topLineValue = topV;
                leftLineValue = leftV - lineSize;
            } else if (i === max - 1) {
                height = topV - topLineValue;
            }
        }
        //多子节点时，需要多一条纵向中间线连接各子节点
        var lineYObj = $("<div class=\"conn_line\"></div>").appendTo(contentObj);
        lineYObj.css({ "border-color": lineColor[level], "height": height + "px", "top": topLineValue + "px", "left": leftLineValue + "px" });
    }

}
//设置单子节点位置
function SetSingleOffset(root, singleArray) {
    var canvs = $(".Area");
    ExceptOffset = canvs.offset();
    MaxWidth = canvs.width() - 15;
    var rootWidth = root.outerWidth(),//根节点完全宽度
        rootHeight = root.outerHeight(),//根节点完全高度
        level = singleArray.length;//级数;
    //计算内容宽度
    ContentWidth = 0 + rootWidth;
    for (var i = 0; i < level; i++) {
        var width = 0;
        $(singleArray[i]).each(function () {
            var temp = $(this).outerWidth();
            if (temp > width) width = temp;
        })
        ContentWidth += width;
    }
    //计算横向间隔
    offsetLeft = (MaxWidth - ContentWidth) / level / 2;
    offsetLeft = offsetLeft > DefaultLeft ? DefaultLeft : offsetLeft;
    //计算根节点左偏移量
    leftItemValue = (MaxWidth - ContentWidth - level * offsetLeft) / 2;
    //计算画布高度
    ContentHeight = 0;
    $(singleArray[level - 1]).each(function () {
        ContentHeight += $(this).outerHeight();
    })
    ContentHeight += DefaultTop * (singleArray[level - 1].length - 1);
    MaxHeight = ContentHeight < rootHeight + DefaultTop * 2 ? rootHeight + DefaultTop * 2 : ContentHeight + DefaultTop * 2;
    //设置画布高度
    canvs.height(MaxHeight);
    //计算根节点上偏移量
    topItemValue = (MaxHeight - rootHeight) / 2;
    //设置根节点偏移量
    root.css({ "top": topItemValue + "px", "left": leftItemValue + "px" });
    var rootLeft = leftItemValue;
    //设置叶节点偏移量
    for (var i = 0; i < level; i++) {
        var items = singleArray.pop();
        if (items.length) {
            //计算当前级别节点最大宽度
            var maxItemWidth = 0;
            $(items).each(function () {
                var temp = $(this).outerWidth();
                if (temp > maxItemWidth) maxItemWidth = temp;
            });
            //设置叶节点偏移量
            topItemValue = (MaxHeight - ContentHeight) / 2;
            $(items).each(function () {
                var child = $("[pid='" + $(this).prop("id") + "']");
                if (child.length > 0) {
                    var childTop = $(child).first().offset().top,
                        childLeft = $(child).first().offset().left,
                        childBottom = $(child).last().offset().top + $(child).last().outerHeight();
                    topItemValue = childTop + (childBottom - childTop - $(this).outerHeight()) / 2 - ExceptOffset.top;
                    leftItemValue = childLeft - offsetLeft - maxItemWidth;
                    $(this).css({ "top": topItemValue + "px", "left": leftItemValue + "px" });
                } else {
                    leftItemValue = rootLeft + level * offsetLeft + ContentWidth - maxItemWidth;
                    $(this).css({ "top": topItemValue + "px", "left": leftItemValue + "px" });
                }
                topItemValue += $(this).outerHeight() + offsetTop;
            })
        }
    }
}
//设置多子节点位置
function SetMoreItemOffset(root, leftArray, rightArray) {
    var canvs = $(".Area");
    ExceptOffset = canvs.offset();
    MaxWidth = canvs.width() - 15;
    $(".Area").width(MaxWidth);
    var rootWidth = root.outerWidth(),//根节点完全宽度
        rootHeight = root.outerHeight(),//根节点完全高度
        level = leftArray.length,
        MaxItemWidthArray = [],//各级最大宽度
        LeftHeightArray = [],//左侧各级高度
        RightHeightArray = [],//右侧各级高度
        leftHeight = 0,//左侧高度
        rightHeight = 0;//右侧高度
    //计算内容宽度
    ContentWidth = 0 + rootWidth;
    for (var i = 0; i < level; i++) {
        var width = 0, tempAllHeight = 0;
        $(leftArray[i]).each(function () {
            var tempW = $(this).outerWidth();
            if (tempW > width) width = tempW;
            tempAllHeight += $(this).outerHeight();
        })
        LeftHeightArray.push(tempAllHeight);
        tempAllHeight += DefaultTop * (leftArray[i].length - 1);
        if (tempAllHeight > leftHeight) leftHeight = tempAllHeight;

        tempAllHeight = 0;
        $(rightArray[i]).each(function () {
            var tempW = $(this).outerWidth();
            if (tempW > width) width = tempW;
            tempAllHeight += $(this).outerHeight();
        })
        RightHeightArray.push(tempAllHeight);
        tempAllHeight += DefaultTop * (rightArray[i].length - 1);
        if (tempAllHeight > rightHeight) rightHeight = tempAllHeight;

        MaxItemWidthArray.push(width);
        ContentWidth += width * 2;
    }
    //计算横向间隔
    offsetLeft = (MaxWidth - ContentWidth) / level / 2;
    if (offsetLeft > DefaultLeft) {
        offsetLeft = DefaultLeft;
    }
    ContentWidth += offsetLeft * level * 2;
    //计算根节点左偏移量
    leftItemValue = (MaxWidth - rootWidth) / 2;
    //计算画布高度
    ContentHeight = 0;
    ContentHeight = leftHeight > rightHeight ? leftHeight : rightHeight;
    MaxHeight = ContentHeight < rootHeight + DefaultTop * 2 ? rootHeight + DefaultTop * 2 : ContentHeight + DefaultTop * 2;
    //设置画布高度
    canvs.height(MaxHeight);
    //计算根节点上偏移量
    topItemValue = (MaxHeight - rootHeight) / 2;
    //设置根节点偏移量
    root.css({ "top": topItemValue + "px", "left": leftItemValue + "px" });
    var rootLeft = leftItemValue;

    for (var i = 0; i < level; i++) {
        //左部
        offsetTop = DefaultTop;
        var leftItemArray = leftArray.pop();
        var leftLevelHeight = LeftHeightArray.pop();
        if (leftItemArray.length) {
            //设置叶节点偏移量
            topItemValue = (MaxHeight - leftHeight) / 2;
            offsetTop = (leftHeight - leftLevelHeight) / (leftItemArray.length - 1);
            $(leftItemArray).each(function () {
                var arrIdx = level - i - 1;
                var child = $("[pid='" + $(this).prop("id") + "']");
                if (child.length > 0) {
                    var childTop = $(child).first().offset().top,
                        childWidth = $(child).first().outerWidth(),
                        childLeft = $(child).first().offset().left,
                        childBottom = $(child).last().offset().top + $(child).last().outerHeight();
                    topItemValue = childTop + (childBottom - childTop - $(this).outerHeight()) / 2 - ExceptOffset.top;
                    leftItemValue = childLeft + childWidth + offsetLeft - ExceptOffset.left + (MaxItemWidthArray[arrIdx] - $(this).outerWidth());
                    $(this).css({ "top": topItemValue + "px", "left": leftItemValue + "px" });
                } else {
                    leftItemValue = (MaxWidth - ContentWidth) / 2 + MaxItemWidthArray[arrIdx] - $(this).outerWidth();
                    $(this).css({ "top": topItemValue + "px", "left": leftItemValue + "px" });

                }
                topItemValue += $(this).outerHeight() + offsetTop;
            })
        }
        //左部

        //右部
        offsetTop = DefaultTop;
        var rightItemArray = rightArray.pop();
        var rightLevelHeight = RightHeightArray.pop();
        if (rightItemArray.length) {
            //设置叶节点偏移量
            topItemValue = (MaxHeight - rightHeight) / 2;
            offsetTop = (rightHeight - rightLevelHeight) / (rightItemArray.length - 1);
            $(rightItemArray).each(function () {
                var arrIdx = level - i - 1;
                var child = $("[pid='" + $(this).prop("id") + "']");
                if (child.length > 0) {
                    var childTop = $(child).first().offset().top,
                        childLeft = $(child).first().offset().left,
                        childBottom = $(child).last().offset().top + $(child).last().outerHeight();
                    topItemValue = childTop + (childBottom - childTop - $(this).outerHeight()) / 2 - ExceptOffset.top;
                    leftItemValue = MaxWidth - childLeft + offsetLeft + ExceptOffset.left + (MaxItemWidthArray[arrIdx] - $(this).outerWidth());
                    $(this).css({ "top": topItemValue + "px", "right": leftItemValue + "px" });
                } else {
                    leftItemValue = (MaxWidth - ContentWidth) / 2 + (MaxItemWidthArray[arrIdx] - $(this).outerWidth());
                    $(this).css({ "top": topItemValue + "px", "right": leftItemValue + "px" });
                }
                topItemValue += $(this).outerHeight() + offsetTop;
            })
        }
        //右部
    }
}
function BuilderArray(targetArray, arr, level) {
    var rA = [];
    var contentObj = $(arr[0]).parents("div.Area");
    var levelItem = contentObj.find("[level='" + level + "']");
    $(arr).each(function () {
        var item = $(this);
        var child = $("[pid='" + item.prop("id") + "']");
        if (child.length)
            $(child).each(function () { rA.push(this); });
        else {
            if (levelItem.length) {
                var nullChild = $("<div class=\"item_null\"></div>").appendTo(contentObj);
                nullChild.height(item.outerHeight());
                nullChild.attr({ "id": "n_" + MISSY.CreateGuid(), "pid": item.prop("id"), "level": level });
                rA.push(nullChild[0]);
            }
        }
    })
    if (rA.length) {
        targetArray.push(rA);
        BuilderArray(targetArray, rA, level + 1);
    }
}