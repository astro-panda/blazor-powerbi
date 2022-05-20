export function embedReport(container, reportId, embedUrl, token) {
    let models = window['powerbi-client'].models;

    let config = {
        type: 'report',
        id: reportId,
        embedUrl: embedUrl,
        accessToken: token,
        permissions: models.Permissions.All,
        tokenType: models.TokenType.Embed,
        viewMode: models.ViewMode.View,
        settings: {
            panes: {
                filters: {
                    expanded: false,
                    visible: false
                },
                pageNavigation: { visible: true }
            }
        }
    };

    let report = powerbi.embed(container, config);

    let heightBuffer = 32;
    let newHeight = $(window).height() - ($('header').height() + heightBuffer);

    $(container).height(newHeight);

    //$(window).resize(() => {

    //});
}