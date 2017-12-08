//为什么用require.js?
//1、模块化编程思维（代码复用、防止全局变量、解决JS文件的依赖关系。）
//2、解决JS阻塞浏览器渲染、性能得以提升(注：谷歌最新浏览器已经可以并行下载js文件了。)
//3、按需加载资源(JS)文件。
var locale = "zh-cn";
require.config({
    baseUrl: "/Scripts",//baseUrl + paths
    map: {
        '*': {
            'css': 'require/require-css/0.1.8/css'//配置require-css插件路径
        }
    },
    urlArgs: "v=1.0.0.1",
    paths: {
        "$": "jquery/jquery/1.11.3/jquery",
        "jquery": "jquery/jquery/1.11.3/jquery",
        "jquery.md5": "jquery/md5/1.2.1/jquery.md5",
        "layer": "layer/layer/3.0.1/layer",
        "MISSY": "missy/2.0.0/missy",
        "MeidaGrid": "MeidaGrid/MeidaGrid",
        "Enum": "JsEnum/EnumHelper",
        "jquery.form": "jquery/form/3.51.0/jquery.form",
        "jquery.scrollUp": "jquery/scrollup/2.4.1/jquery.scrollUp",
        "jquery.SuperSlide": "jquery/SuperSlide/2.1.1/jquery.SuperSlide",
        "jquery.autocomplete": "jquery/autocomplete/1.2.26/jquery.autocomplete.min",
        "jquery.ztree": "zTree/3.5.24/jquery.ztree.all.min",
        "ueditor": "ueditor/1.4.2/ueditor.all",
        "ueditor.config": "ueditor/1.4.2/ueditor.config.js?v=20170623",
        'ueditor.lang': "ueditor/1.4.2/lang/" + locale + "/" + locale,
        "ueditor.ZeroClipboard": "ueditor/1.4.2/third-party/zeroclipboard/ZeroClipboard.min",
        "i18n": "require/require-i18n/2.0.6/i18n",
        "jquery.lazyload": "jquery/lazyload/1.9.7/jquery.lazyload.min",
        "My97": "My97/WdatePicker",
        "ICheck": "icheck/icheck.min",
        "WebUploader": "webuploader/0.1.5/webuploader.min",
        "Calendar": "Calendar/calendar",
        "CustomScroll": "CustomScroll/scroll",
        "select.Suggest": "select/1.0.0/select.Suggest",
        "parentHelper": "CustomParent/parentHelper",
        "customDraw": "CustomDraw/customdraw"
    },
    shim: {
        "jquery.md5": { deps: ["jquery"], exports: 'jQuery.fn.md5' },
        "layer": { deps: ["jquery", "css!/Scripts/layer/layer/3.0.1/skin/default/layer.css"] },
        "MISSY": { deps: ["jquery", "layer"] },
        "parentHelper": { deps: ["jquery", "layer", "MISSY"] },
        "customDraw": { deps: ["jquery"] },
        "MeidaGrid": { deps: ["jquery", "css!/Scripts/MeidaGrid/MeidaGrid.css"] },
        "jquery.form": { deps: ["jquery"] },
        "jquery.scrollUp": { deps: ["jquery", "css!/Scripts/jquery/scrollup/2.4.1/skin/default.css"], exports: 'jQuery.fn.scrollUp' },
        "jquery.SuperSlide": { deps: ["jquery"], exports: 'jQuery.fn.slide' },
        "jquery.autocomplete": { deps: ["jquery", "css!/Scripts/jquery/autocomplete/1.2.26/skin/default.css"] },
        "jquery.ztree": { deps: ["jquery", "css!/Scripts/zTree/3.5.24/skin/zTreeStyle/zTreeStyle.css"], exports: 'jQuery.fn.zTree' },
        "ueditor": { deps: ["ueditor.config", "css!/Scripts/ueditor/1.4.2/themes/default/css/ueditor.min.css"], exports: "UE" },
        "ueditor.lang": { deps: ["ueditor"] },
        "jquery.lazyload": { deps: ["jquery"], exports: "jQuery.fn.lazyload" },
        "ICheck": { deps: ["jquery", "css!/Scripts/icheck/skins/square/blue.css"] },
        "WebUploader": { deps: ["css!/Scripts/webuploader/0.1.5/webuploader.css"] },
        "Calendar": { deps: ["jquery"] }
    },
    config: {
        i18n: { locale: locale }
    },
    waitSeconds: 30
});
