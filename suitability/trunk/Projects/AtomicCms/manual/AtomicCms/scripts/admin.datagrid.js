$(document).ready(function()
{
    highlightTableRowOnMouseOver();
});

function highlightTableRowOnMouseOver()
{
    $('table.dataGrid tr:odd').addClass('odd');

    $('table.dataGrid tr').mouseover(function()
    {
        $(this).addClass('mouseOver');
    }).mouseout(function()
    {
        $(this).removeClass('mouseOver');
    });
}