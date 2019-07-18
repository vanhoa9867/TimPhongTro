var common = {
    init: function () {
        common.registerEvent();
    },
    registerEvent: function () {
        $("input").change({
            console.log($("#txtkeyword").val());
            minLength: 0,
            source: function (request, response) {
                $.ajax({
                    url: "/TimKiem/ListName",
                    dataType: "jsonp",
                    data: {
                        term: request.term
                    },
                    success: function (res) {
                        response(res.data);
                    }
                });
            },
            focus: function (event, ui) {
                $("#txtkeyword").val(ui.item.label);
                return false;
            },
            select: function (event, ui) {
                $("#txtkeyword").val(ui.item.label);
                return false;
            }
        })
            .autocomplete("instance")._renderItem = function (ul, item) {
                return $("<li>")
                    .append("<div>" + item.label + "</div>")
                    .appendTo(ul);
            };
        } );
    }
}

common.init();