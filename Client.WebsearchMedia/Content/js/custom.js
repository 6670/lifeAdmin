(function ($, window, Typist) {
    
	$(document).ready(function(){
		/*---------------scroll-top-------------*/
	  
		$('.gototop-JS').on("click",function(){
			$("html, body").animate({ scrollTop: 0 }, 800);
			return false;
		});
		
		/*----expand_div-------*/
		
		$(".callButton").click(function() {
		  $('.callbackform').slideToggle().toggleClass('active');
		  
		  //if ($('.callbackform').hasClass('active')) {
			//$('.callButton').text('Collapse');
		  //} else {
			//$('.callButton').text('Expand');
			
		  //}
		  if($(window).width() > 890){
			disableScroll();
		  }
		});

	    $('.close_Div-js').click(function() {
	        $(".callButton").trigger('click');
	        enableScroll();
	    });

	});
	var keys = {37: 1, 38: 1, 39: 1, 40: 1};

	function preventDefault(e) {
	  e = e || window.event;
	  if (e.preventDefault)
		  e.preventDefault();
	  e.returnValue = false;  
	}

	function preventDefaultForScrollKeys(e) {
		if (keys[e.keyCode]) {
			preventDefault(e);
			return false;
		}
	}
	function enableScroll() {
		if (window.removeEventListener)
			window.removeEventListener('DOMMouseScroll', preventDefault, false);
		window.onmousewheel = document.onmousewheel = null; 
		window.onwheel = null; 
		window.ontouchmove = null;  
		document.onkeydown = null;  
	}
	function disableScroll() {
	  if (window.addEventListener) // older FF
		  window.addEventListener('DOMMouseScroll', preventDefault, false);
	  window.onwheel = preventDefault; // modern standard
	  window.onmousewheel = document.onmousewheel = preventDefault; // older browsers, IE
	  window.ontouchmove  = preventDefault; // mobile
	  document.onkeydown  = preventDefaultForScrollKeys;
	}
	
	/*-------------scroll_to_Fixed-----------------*/
	
	function isVisible(elment) {
		
		var vpH = $(window).height(), // Viewport Height
			st = $(window).scrollTop(), // Scroll Top
			y = $(elment).offset().top;
     
		return y <= (vpH + st);
	}

	function setSideNotePos(){
		$(window).scroll(function() {
			if (isVisible($('.footer_JS'))) {
				$('.affix-top').css('position','absolute');
				$('.affix-top').css('top',$('.footer_JS').offset().top - $('.affix-top').outerHeight() - 150);
			} else {
				$('.affix-top').css('position','fixed');
				$('.affix-top').css('top','auto');
			}
		});
	}
	$(document).ready(function() {
		setSideNotePos();
	});

})(jQuery, window);