/* 
* @Author: nichenqi
* @Date:   2016-05-14 09:52:41
* @Last Modified by:   nichenqi
* @Last Modified time: 2016-05-25 20:08:20
*/

'use strict';

/*banner*/
$(function() {
	
	function banner(box){
		var box=$(box),//最大轮播图宽度必须给
			rightBtn=box.find('.rightBtn'),//右箭头名字
			leftBtn=box.find('.leftBtn'),//左箭头名字
			pic=box.find('.imgList'),//轮播图ul名字			
			dotBtn=box.find('.btnList'),//轮播点名字
			btnList=dotBtn.find('li'),
			imgListSon=pic.find('li'),
			num=0,
			imgWidth=box.width(),
			timer=null,
			picWidth=box.width(),
			lingshi=pic.children().eq(0).clone(true),
			indexNum=0,
			flag=true;

		pic.width(pic.width()+picWidth);
		pic.append(lingshi);

		function gogo(){
			if(indexNum>imgListSon.length-1){
				indexNum=0
			}
				pic.stop().animate({
					'left':-imgWidth*num,
				},500,function(){
					flag=true;
				})
				btnList.eq(indexNum).addClass('current').siblings().removeClass('current');			
		}
		function rightgo(){
			num++;
			indexNum++;
			if(num>imgListSon.length){

				pic.css('left',0);
				num=1;
				
			}
			gogo();	
		}
		function leftgo(){
			num--;
			indexNum--;
			if(indexNum<0){
				indexNum=imgListSon.length-1
			}
			if(num<0){
				num=imgListSon.length-1;
				pic.css('left',-pic.width()+picWidth);
						
			}

			gogo();	
		}

		rightBtn.click(function(event) {
			if(flag){
				rightgo()
				flag=false;
			}			
		});

		leftBtn.click(function(event) {
			if(flag){
				leftgo()
				flag=false;
			}	
		});

		btnList.click(function(event) {
			num=$(this).index();
			indexNum=$(this).index();
			gogo();
		});

		timer=setInterval(rightgo, 1500)

		box.hover(function() {

			clearInterval(timer);
		}, function() {
			clearInterval(timer);
			timer=setInterval(rightgo, 1500)
		});
		}

	banner('.banner1');
	banner('.floor1 .floor1-banner');
	banner('.floor2 .floor2-banner');
	banner('.floor3 .floor3-banner');
	banner('.floor4 .floor4-banner');
});



/*箭头 品质生活*/
$(function() {
	
	/*箭头*/
	var arrow=$('.arrow');
	arrow.hide();
	arrow.parent().hover(function(event) {
		$(this).children('.arrow').toggle();
	});
	/*今日推荐箭头*/
	var y=0;
	var wareUl=$('.wareMain').children();
	$('.recommend .prev').click(function(event) {

		if(y<wareUl.length-1){
			y++;
			$('.wareMain').stop().animate({'left':-1000*y},500);
			
		}else{
			y=0;
			$('.wareMain').css('left',1000);
			$('.wareMain').stop().animate({'left':-1000*y},500);			
		}
	})
	$('.recommend .next').click(function(event) {	

		if(y<1){
			y=wareUl.length-1;
			$('.wareMain').css('left',-1000*wareUl.length);
			$('.wareMain').stop().animate({'left':-1000*y},500);
		}else{
			y--;
			$('.wareMain').stop().animate({'left':-1000*y},500);
		}
	})
	/*今日推荐*/
	$('.recommend ul li:eq(3)').css({'margin-right':0,'width':250});
	$('.recommend ul li:eq(3) img').css('width',250);
	/*品质生活*/
	// 第1块
	$('.life .main1 .big').hover(function() {
		$('.life .main1 .big img').stop().animate({'left':-8},200)
	}, function() {
		$('.life .main1 .big img').stop().animate({'left':8},200)
	});

	$('.life .small').eq(0).css('border-right','1px solid #ededed')
	// 第3块
	$('.life .main3 .big').hover(function() {
		$('.life .main3 .big img').stop().animate({'left':-8},200)
	}, function() {
		$('.life .main3 .big img').stop().animate({'left':8},200)
	});
	// 第4块
	
	var lifeLi=$('.life .main4 li');
	for(var i=1;i<lifeLi.length;i+=2){
		lifeLi.eq(i).css('border-right','none');
	}
	lifeLi.eq(0).css('height',44);
	lifeLi.eq(1).css('height',44);
	lifeLi.eq(12).css({'height':44,'border-bottom':'none'});
	lifeLi.eq(13).css({'height':44,'border-bottom':'none'});



	/*floor header*/
	var floorHead1=$('.floor1 .header li:last'),
		floorHead2=$('.floor2 .header li:last'),
		floorHead3=$('.floor3 .header li:last'),
		floorHead4=$('.floor4 .header li:last');

	floorHead1.css('border-right-color','#fff')
	floorHead1.hover(function() {
		$(this).css('border-right-color','#b61d1d')
	}, function() {
		$(this).css('border-right-color','#fff')
	});

	floorHead2.css('border-right-color','#fff')
	floorHead2.hover(function() {
		$(this).css('border-right-color','#b61d1d')
	}, function() {
		$(this).css('border-right-color','#fff')
	});

	floorHead3.css('border-right-color','#fff')
	floorHead3.hover(function() {
		$(this).css('border-right-color','#b61d1d')
	}, function() {
		$(this).css('border-right-color','#fff')
	});

	floorHead4.css('border-right-color','#fff')
	floorHead4.hover(function() {
		$(this).css('border-right-color','#b61d1d')
	}, function() {
		$(this).css('border-right-color','#fff')
	});
});



/*floor图片切换*/
$(function() {
	
	function floorNav(box){

		var nav=$(box),
			floorNav=nav.find('.floor-nav li'),
			floorMain=nav.find('.floor-main');

		floorMain.first().show()
		floorNav.hover(function() {
			
			var index=$(this).index();
			$(this).addClass('current').siblings().removeClass('current');
			floorMain.eq(index).show().siblings().hide();


		}, function() {
			
		});

	}

	floorNav('.floor1')
	floorNav('.floor2')
	floorNav('.floor3')
	floorNav('.floor4')
});

/*固定电梯*/
$(function() {

	
	var fix=$('.fixeder'),
		fixLi=fix.find('li'),
		floor=$('.floor'),
		floor1=$('.floor1').offset().top,
		floor2=$('.floor2').offset().top,
		floor3=$('.floor3').offset().top,
		floor4=$('.floor4').offset().top;

	fix.css('margin-top',-fix.height()/2);


	$(window).scroll(goFloor);

	fixLi.click(function(event) {
		
		$(window).off('scroll');

		var index=$(this).index();

		var floorOffset=floor.eq(index).offset().top;

		$('html,body').stop().animate({'scrollTop':floorOffset},500,function(){
			$(window).scroll(goFloor);
			fixLi.eq(index).addClass('current').siblings().removeClass('current');
		})
	});


	function go(index){
		
		fix.fadeIn();
		fixLi.eq(index).addClass('current').siblings().removeClass('current');
	}

		

	function goFloor(){

		var scrolltop=$(window).scrollTop();
		
		if(scrolltop>floor4-100){
			go(3);
		}else if(scrolltop>floor3-100){
			go(2);
		}else if(scrolltop>floor2-100){
			go(1);
		}else if(scrolltop>floor1-100){
			go(0);
		}else{
			fix.fadeOut();
		}
	}
});

/*京东快报*/
$(function() {
	
	var iphone=$('.iphone'),
		listBtn=iphone.find('.fRow'),
		liftservList=iphone.find('.liftserv-list'),
		liftservListUl=liftservList.find('ul'),
		liftservList2=liftservListUl.children('li'),
		liftservListP=liftservList.children('p'),
		oHide=liftservListP.children('b'),
		flag=true,
		flag2=true,
		timer=null;

		
		listBtn.hover(function() {

			var x=$(this).index();
			liftservListP.hide().eq(x).show();
			liftservList2.removeClass('current').eq(x).addClass('current')
			timer=setTimeout(function(){
				flag2=true;
				
				if(flag && flag2){

					liftservList.stop().animate({
						'top':72
					},200,function(){

						liftservListUl.stop().animate({
							'top':-29
						},300);
						liftservList.stop().animate({
							'top':29
						},300);

						flag=false;
						flag2=false;
						
					});
				}
			}, 400)

		}, function() {
			clearTimeout(timer);
			flag=true;
		});

		liftservList2.hover(function() {
			
			var index=$(this).index();
			liftservListP.hide().eq(index).show();
			$(this).addClass('current').siblings().removeClass('current')
		}, function() {
			/* Stuff to do when the mouse leaves the element */
		});

		oHide.click(function(event) {
			liftservList.stop().animate({
				'top':210
			},300);
			liftservListUl.stop().animate({
				'top':0
			},300);

			flag=false;
				
		});

});