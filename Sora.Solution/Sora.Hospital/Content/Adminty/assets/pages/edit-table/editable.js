  'use strict';
 $(document).ready(function() {  
     if ($('#configtable').length) {
         $('#configtable').Tabledit({
             url: "/admin/SAConfig/Update",
             deleteButton: false,
             hideIdentifier: true,
             columns: {
                 identifier: [0, 'Id'],
                 editable: [[2,"ConfigValue"]]
             },
             onSuccess: function (data) {
                 console.log(data);
                 if (data.status === "success") {
                     $("#config-message-success").html(data.message);
                     $("#config-alert-success").show();
                 }
                 else {
                     $("#config-message-fail").html(data.message);
                     $("#config-alert-fail").show();
                 }
             },
             onError: function (data) {
                 console.log(data);
             }
         });
     }
     if ($('.translatetable').length) {
         $('.translatetable').Tabledit({
             url: "/admin/Translate/Update",            
             deleteButton: false,
             hideIdentifier: true,
             columns: {
                 identifier: [0, 'Id'],
                 editable: [[2, "CMSValue"]]
             },
             onSuccess: function (data) {
                 console.log(data);
                 if (data.status === "success") {
                     $("#config-message-success").html(data.message);
                     $("#config-alert-success").show();
                 }
                 else {
                     $("#config-message-fail").html(data.message);
                     $("#config-alert-fail").show();
                 }
             },
             onError: function (data) {
                 console.log(data);
             }
         });
     }
});
