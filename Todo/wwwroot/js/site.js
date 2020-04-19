// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
let todoListDataTable;
$(document).ready(function () {
    $('.auto-submit').on('click', function (e) {
        const form = e.target.closest('form');
        form.submit();
    });

    $.fn.dataTable.ext.type.order['Importance'] = function (d) {
        switch (d) {
            case 'Low':
                return 1;
            case 'Medium':
                return 2;
            case 'High':
                return 3;
        }
        return 0;
    };

    todoListDataTable = $('#todo-list').DataTable({
        "paging": false,
        "info": false,
        "searching": false,
        "columnDefs": [
            {
                "targets": [2],
                "visible": false
            }
        ],
        "order": [[2, "asc"]]
    });

    $("[email-hash]").each(function () {
        const self = $(this);

        const hash = self.attr('email-hash');
        const email = self.attr('email');
        const url = "https://pl.gravatar.com/" + hash + ".json";
        $.get(url, function () {
        })
            .done(function (x) {
                const profileData = x.entry[0];
                let stringToDisplay = "";

                if (profileData != null) {
                    stringToDisplay = profileData.displayName;
                }
                stringToDisplay += " - " + email;
                self.text(stringToDisplay);
            })
            .fail(function () {
                self.text(email);
            })
            .always(function () {
            });

    });
});

function patchRank(element, todoItemId, rankIncrement) {
    const rankValueContainer = $($(element).parent()).children('span');
    const url = "/api/to-do-item/patch-rank/" + todoItemId;
    const newRankValue = parseInt(rankValueContainer.text()) + rankIncrement;
    $.ajax({
        type: 'PATCH',
        url: url,
        data: JSON.stringify({rank: newRankValue}),
        processData: false,
        contentType: 'application/json-patch+json'
    }).done(function (x) {
        rankValueContainer.text(newRankValue);
        const rowId = "item-" + todoItemId;
        todoListDataTable.row('[id="' + rowId + '"]').invalidate().draw();
    });

}