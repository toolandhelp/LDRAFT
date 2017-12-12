//////----TO TOP---////////
jQuery(document).ready(function ($) {
jQuery('.totop').click(function(){
	jQuery('html, body').animate({ scrollTop: 0 }, "slow");
});
});	

//////----MENU---////////
jQuery(document).ready(function ($) {
$('.navbar .dropdown').hover(function() {
	$(this).addClass('extra-nav-class').find('.dropdown-menu').first().stop(true, true).delay(250).slideDown();
}, function() {
	var na = $(this)
	na.find('.dropdown-menu').first().stop(true, true).delay(100).slideUp('fast', function(){ na.removeClass('extra-nav-class') })
});

$('.dropdown-submenu').hover(function() {
	$(this).addClass('extra-nav-class').find('.dropdown-menu').first().stop(true, true).delay(250).slideDown();
}, function() {
	var na = $(this)
	na.find('.dropdown-menu').first().stop(true, true).delay(100).slideUp('fast', function(){ na.removeClass('extra-nav-class') })
});

});	

	
//////CONTACT FORM VALIDATION //问题提交
jQuery(document).ready(function ($) {

    $("#refresh").click(function () { ClickValidateCode(); });

   //问题图片验证码
    $("#imgcode").click();
    $("#imgcode").click(function () { ClickValidateCode(); }).click();
    //验证码
    function ClickValidateCode() {
        $('.done').hide(); //成功
        $('#done_warning').hide(); //警告
        $('#done_error').hide(); //错误

        $("#Qcode").val("");
        $("#imgcode").prop('src', '/Home/CodeImg?_r=' + Math.random());
    }

	//if submit button is clicked
    $('#submit').click(function () {	

        $('.done').hide(); //成功
        $('#done_warning').hide(); //警告
        $('#done_error').hide(); //错误
        //$("after_submit").remove();

		//Get the data from all the fields
		var name = $('input[name=name]');
		var email = $('input[name=email]');
		var regx = /^([a-z0-9_\-\.])+\@([a-z0-9_\-\.])+\.([a-z]{2,4})$/i;
        var comment = $('textarea[name=comment]');
        var qcode = $('input[name=Qcode]');
		var returnError = false;
		
		//Simple validation to make sure user entered something
		//Add your own error checking here with JS, but also do some error checking with PHP.
		//If error found, add hightlight class to the text field
		if (name.val()=='') {
			name.addClass('error');
			returnError = true;
		} else name.removeClass('error');
		
		if (email.val()=='') {
			email.addClass('error');
			returnError = true;
		} else email.removeClass('error');		
		
		if(!regx.test(email.val())){
          email.addClass('error');
          returnError = true;
		} else email.removeClass('error');
		
		
		if (comment.val()=='') {
			comment.addClass('error');
			returnError = true;
		} else comment.removeClass('error');

        if (qcode.val() == '') {
            qcode.addClass('error');
            returnError = true;
        } else qcode.removeClass('error');

		// Highlight all error fields, then quit.
		if(returnError == true){
			return false;	
		}
		
		//organize the data

        var data = 'name=' + name.val() + '&email=' + email.val() + '&comment=' + encodeURIComponent(comment.val()) + '&qcode=' + qcode.val();

		//disabled all the text fields
		$('.text').attr('disabled','true');
		
		//show the loading sign
		$('.loading').show();
		
		//start the ajax
		$.ajax({
			//this is the php file that processes the data and sends email
            url: "/Home/Question",	
			
			//GET method is used
            type: "POST",

            //同步
            async: false,

			//pass the data			
			data: data,		
			
			//Do not cache the page
			cache: false,
			
			//success
            success: function (response) {
                //同一个人提交5遍
                if (response.ErrorType == 2) {
                    $('.done').hide(); //成功
                    $('#done_warning').show(); //警告
                    $('#done_error').hide(); //错误
                    return;
                }
				//if returned 1/true (send mail success)
                if (response.ErrorType == 1) {

                    $("after_submit").remove();
                    $("#imgcode").prop('src', '/Home/CodeImg?_r=' + Math.random());
                    //show the success message
                    // $('.done').fadeIn('slow');
                    $('.done').show(); //成功
                    $('#done_warning').hide(); //警告
                    $('#done_error').hide(); //错误

                    $(".form").find('input[type=text], textarea').val("");

                    return;

                    //if returned 0/false (send mail failed)
                } else {
                    $('.done').hide(); //成功
                    $('#done_warning').hide(); //警告
                    $('#done_error').show(); //错误
                    //$("after_submit").remove();
                    //$("#done").after('<label class="alert alert-error" id="after_submit"><button type="button" class="close" data-dismiss="alert">×</button>验证码错误!点击<a harf="javascript:;" id="refresh">验证码</a>,刷新再试一次.</label>');
                }
			}		
		});
		
		//cancel the submit button default behaviours
		return false;
	});	
});	

//////----TEXT ROTATOR---////////
jQuery(document).ready(function ($) {
$('.textItem').quovolver();
  });

//////----Tiny Nav Responsive Menu---////////
 $(function () {
    $("#nav").tinyNav({
  active: 'selected', // String: Set the "active" class
  header: 'NAVIGATION', // String: Specify text for "header" and show header instead of the active item
  label: '' // String: Sets the <label> text for the <select> (if not set, no label will be added)
});
  });
  
  
//////----Placeholder for IE---////////
$(function() {
    // Invoke the plugin
    $('input, textarea').placeholder();
  });
