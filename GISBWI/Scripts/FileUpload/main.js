/*
 * jQuery File Upload Plugin JS Example 6.5.1
 * https://github.com/blueimp/jQuery-File-Upload
 *
 * Copyright 2010, Sebastian Tschan
 * https://blueimp.net
 *
 * Licensed under the MIT license:
 * http://www.opensource.org/licenses/MIT
 */

/*jslint nomen: true, unparam: true, regexp: true */
/*global $, window, document */

$(function () {
    'use strict';

    // Initialize the jQuery File Upload widget:
    $('#fileupload').fileupload({
        // Uncomment the following to send cross-domain cookies:
        //xhrFields: {withCredentials: true},

        //url: '/Files',
        //acceptFileTypes: /(\.|\/)(gif|jpe?g|png)$/i,
        //maxFileSize: 3000000,
    });

    $('#fileupload').fileupload('option', {
            //acceptFileTypes: /(\.|\/)(gif|jpe?g|png|xlsx?)$/i,
        //maxFileSize: 500000000,
            maxFileSize: 500000000,
            resizeMaxWidth: 1920,
            resizeMaxHeight: 1200
        });
});
