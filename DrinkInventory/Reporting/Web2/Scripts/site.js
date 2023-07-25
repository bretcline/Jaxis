// move elements to a new parent
(function ($) {
    $.fn.moveTo = function (selector, clone) {
        
        if (clone == undefined)
            clone = true;
        
        return this.each(function () {
            var cl = $(this).clone(clone);
            $(cl).prependTo(selector);
            $(this).remove();
        });
    }
})(jQuery);

function showAlertMessage(message)
{
    if (message != null && message != '')
    {
        $('#serverMessage').text(message);
        var windowWidth = $(window).width();
        var messageWidth = $('#serverMessage').width();
        var windowHeight = $(window).height();
        var messageHeight = $('#serverMessage').height();
        $('#serverMessage').css({ left: (windowWidth - messageWidth) / 2, top: (windowHeight - messageHeight) / 2 });
        $('#serverMessage').fadeIn(500, function ()
        {
            setTimeout(function ()
            {
                $('#serverMessage').fadeOut(500);
            }, 2000);

        });
    }
}

function callServer(url, data, type, success)
{
    $.ajax(
    {
        url: url,
        data: data,
        type: type,
        traditional: true,
        success: function (response)
        {
            if (response.Status == 0)
            {
                success(response.Data);
            }
            else if (response.Status == 2)
            {
                window.location.href = response.Message;
                return;
            }

            showAlertMessage(response.Message);
        },
        error: function (jqXHR, textStatus, errorThrown)
        {
            showAlertMessage('An error occurred communicating with the web server: ' + errorThrown);
        }
    });
}

function focusFirstInput()
{
    $('input:text:visible:first').focus().select();
}

$(document).ready(function ()
{

    $('.pagecontainer').fadeIn(1000);

    $('nav a.normal').hover(

        function ()
        {
            $(this).animate({ backgroundColor: 'rgb(197,166,112)', color: 'Black', paddingTop: '4px', paddingLeft: '14px', paddingRight: '14px' }, { queue: false, duration: 300 });
        },

        function ()
        {
            $(this).animate({ backgroundColor: '#444444', color: '#cccccc', paddingTop: '1px', paddingLeft: '12px', paddingRight: '12px' }, { queue: false, duration: 300 });
        }

    );


    $('.navbaritem span').hover(

        function ()
        {
            $(this).animate({ backgroundColor: 'rgb(197,166,112)', color: '#ffffff' }, { queue: false, duration: 300 });
        },

        function ()
        {
            $(this).animate({ backgroundColor: 'White', color: 'Black' }, { queue: false, duration: 300 });
        }

    );

    // $('.navbarbody').animate({ fontSize: '12px' }, 500);
    focusFirstInput();
});

