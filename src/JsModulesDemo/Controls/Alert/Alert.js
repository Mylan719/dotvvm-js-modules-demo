/// <reference path="../../Resources/Scripts/typings/Knockout.d.ts" />
export class AlertExtensions {
    static showSuccess(alert, message) {
        console.info(alert());
        console.info(message);
        alert().IsVisible().Value(true);
        alert().Message(message);
        alert().Type("Success");
    }
    static showInfo(alert, message) {
        alert().IsVisible().Value(true);
        alert().Message(message);
        alert().Type("Info");
    }
    static showDanger(alert, message) {
        alert().IsVisible().Value(true);
        alert().Message(message);
        alert().Type("Warning");
    }
    static showWarning(alert, message) {
        alert().IsVisible().Value(true);
        alert().Message(message);
        alert().Type("Danger");
    }
}
window.AlertExtensions = AlertExtensions;
//# sourceMappingURL=Alert.js.map