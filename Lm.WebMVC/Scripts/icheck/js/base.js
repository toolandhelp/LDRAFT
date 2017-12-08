/* 
* @Author: nichenqi
* @Date:   2016-05-25 10:21:46
* @Last Modified by:   nichenqi
* @Last Modified time: 2016-05-25 10:55:18
*/

'use strict';

/*搜索*/
$(function() {
	
	var search=$('.logo .search input');

	search.focus(function(event) {

		if(search.val()=='小龙虾'||search.val()=='请输入搜索内容'){
			$(this).val('')
		}
	});
	search.blur(function(event) {
		
		if(search.val()==''){
			$(this).val('请输入搜索内容');
		}
	});
});

$(function() {
	/*购物车*/
	$('.logo .car').hover(function() {

		$(this).addClass('hover');
		$(this).children('em').toggle();
		$(this).children('p').toggle();
	}, function() {
		$(this).removeClass('hover');
		$(this).children('em').toggle();
		$(this).children('p').toggle();
	});
});

/*送至地点address*/
$(function() {

	var addressList=$('.top .address ul'),
		addspan=$('.top .address span'),
		divBtn=$('.top .address:first'),
		btn=addressList.find('a');

	
		divBtn.hover(function() {
			addressList.show()
		}, function() {
			addressList.hide()
		});

	btn.click(function(event) {
		
		var x=$(this).html();
		btn.removeClass('current');
		$(this).addClass('current');

		addspan.html('送至：'+x);
	});
});