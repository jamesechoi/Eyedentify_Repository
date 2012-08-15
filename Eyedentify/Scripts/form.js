$(document).ready(function(){

	$.each($('form.form'), function(count, form) {
	
		var errorEl = $(form).find('div.output').first();
		var submitEl = $(form).find('button.submit').first();
		var spinnerEl = $(form).find('div.spinner').first();
		
		$('form.form input.swap, form.form textarea.swap').each(function(i, el)
		{
			var el = $(el);
			//el.val(el.parent().find('label').text());
			//el.parent().find('label').hide();
			var el_text = el.attr('value');
	
			el.focus(function()
			{
				if($(this).val()==el_text)
				{
					$(this).val('');
					$(this).addClass('active');
				}
			});
	
			el.blur(function()
			{
				if($(this).val()=='')
				{
					$(this).val(el_text);
					$(this).removeClass('active');
				}
			});
			//console.log(el_text);
		});
		
		$.validator.addMethod("notlabel", function(value, element)
		{
			label = $('label[for='+$(element).attr('id')+']:eq(0)');
			//console.log(label.text());
			regexstring = '^'+label.text()+'$';
			regexp = new RegExp(regexstring, "gi");
			islabel = !regexp.test(value);
			console.log(regexstring+' '+value+' '+islabel);
			return this.optional(element) || (islabel);
		}, "This field is required");
		
		$.validator.addMethod("nottitle", function(value, element)
		{
			//label = $('label[for='+$(element).attr('id')+']:eq(0)');
			//console.log(label.text());
			//regexstring = '^'+label.text()+'$';
			//regexp = new RegExp(regexstring, "gi");
			istitle = !(value == $(element).attr('title'));
			console.log($(element).attr('title')+' '+value+' '+istitle);
			return this.optional(element) || (istitle);
		}, "This field is required");
	
		$(form).attr('action', $(form).attr('action')+'&ajax=1');
		$(form).validate({
			ignoreTitle: true,
			errorContainer: errorEl,
			highlight: function(element, errorClass, validClass) {
				$(element).addClass('error-field');
				$(element.form).find("label[for=" + element.id + "]").addClass('error-label');
			},
			unhighlight: function(element, errorClass, validClass) {
				$(element).removeClass('error-field');
				$(element.form).find("label[for=" + element.id + "]").removeClass('error-label');
			},
			submitHandler: function(form) {
				var responseEl = $(form).find('div.response').first();
				
				$(responseEl).show();
				$(responseEl).remove();
				//$(submitEl).val('Submitting...');
				$(spinnerEl).show();
				//$(submitEl).css('opacity', 0.5);
				$(submitEl).fadeTo("fast", 0.5);
				$(form).ajaxSubmit({
					success: contactResponse
				});
			}
		});
		
		$(form).submit(function() {
			if(!$(form).valid()) {
				$('.tip').poshytip('disable');
				$('.tip').poshytip({
					className: 'tip-twitter',
					showOn: 'focus',
					alignTo: 'target',
					alignX: 'right',
					alignY: 'center',
					offsetX: 0,
					offsetY: 5
				});
			}
		});
	});
	
	
	
	$('.tip').poshytip({
		className: 'tip-twitter',
		showOn: 'focus',
		alignTo: 'target',
		alignX: 'right',
		alignY: 'center',
		offsetX: 0,
		offsetY: 5
	});



});

function contactResponse(responseText, statusText, xhr, $form) {
	
	var spinnerEl = $form.find('div.spinner').first();
	var submitEl = $form.find('button.submit').first();
	var topEl = $('.form-top').first();
	$('div.response').remove();
	var responseEl = $('<div class="response"></div>');
	responseEl.insertBefore($form);
	
	var json = eval('('+responseText+')');
	if(json.error) {
		var errorList = '<ol>';
		$.each(json.error.messages, function(i, el){
			errorList += '<li>'+el+'</li>';
		});
		errorList += '</ol>';
		if($form.attr('rel') == 'no-error') {
			$(responseEl).html('<div>'+errorList+'</div>');
		} else {
			$(responseEl).html('<div><p>'+json.error.result+'</p>'+errorList+'</div>');//There seems to have been an error with your submission.</div>');
		}
	} else {
		
		if(json.result.result) {
			if(json.result.messages.ping){
				$form.fadeTo(400, 0.1);
				$(responseEl).html('<div><div class="spinner redirect_spinner"></div><p>Redirecting &#8230;</p></div>');
				$(responseEl).show();
				$form.slideUp('slow');
				window.location = json.result.messages.ping;
			} else if (json.result.messages.dataset){
				if (json.result.messages.dataset.callback){
					var dataset = json.result.messages.dataset.data;
					var callback = json.result.messages.dataset.callback;
					eval(callback)(dataset);
					$(submitEl).fadeTo("fast", 1);
				}
			} else {
				$form.fadeTo(400, 0.1);
				var messageList = '<ol>';
				$.each(json.result.messages, function(i, el){
					messageList += '<li>'+el+'</li>';
				});
				messageList += '</ol>';
				
				if($form.attr('rel') == 'no-msg') {
					$(responseEl).html('<div>'+messageList+'</div>');
				} else {
					$(responseEl).html('<div><p>'+json.result.result+'</p>'+messageList+'</div>');
				}
				
				$(responseEl).show();
				$form.slideUp('slow');
			}
		} else {
			$(responseEl).html('<div>There seems to have been an error with your submission.</div>');
		}
	}
	window.location.hash = topEl.attr('name');
	$(spinnerEl).hide();
}
