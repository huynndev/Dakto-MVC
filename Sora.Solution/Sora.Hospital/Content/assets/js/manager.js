function parseJsonDate(jsonDateString) {
    if (jsonDateString != null && jsonDateString !== "undefined") {
        var date = new Date(parseInt(jsonDateString.replace('/Date(', '')));
        return (date.getDate() + '-' + (date.getMonth() + 1) + '-' + date.getFullYear());
    }
    return jsonDateString;
}