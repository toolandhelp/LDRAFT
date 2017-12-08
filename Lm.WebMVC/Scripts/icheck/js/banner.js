/* 
* @Author: nichenqi
* @Date:   2016-05-22 18:35:33
* @Last Modified by:   nichenqi
* @Last Modified time: 2016-05-25 01:37:24
*/

'use strict';

/*banner*/
$(function() {
	
	function banner(box){
		var box=$(box),
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

		pic.append(lingshi);
		var imgListSon2=pic.find('li');
		pic.width(imgListSon2.length*100+'%');
		imgListSon2.width(1/(imgListSon2.length)*100+'%');

		console.log(pic.width())
		

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

	banner('.list-banner');
});	