/// <reference path="../../Resources/Scripts/typings/Knockout.d.ts" />

export interface AlertModel {
    Type: KnockoutObservable<string>;
    Message: KnockoutObservable<string>;
    IsVisible: KnockoutObservable<ControlProperty<boolean>>;
}

export interface ControlProperty<TValue> {
    Value: KnockoutObservable<TValue>;
}

export class AlertExtensions {
    public static showSuccess(alert: KnockoutObservable<AlertModel>, message: string): void {
        console.info(alert());
        console.info(message);
        alert().IsVisible().Value(true);
        alert().Message(message);
        alert().Type("Success");
    }

    public static showInfo(alert: KnockoutObservable<AlertModel>, message: string): void {
        alert().IsVisible().Value(true);
        alert().Message(message);
        alert().Type("Info");
    }

    public static showDanger(alert: KnockoutObservable<AlertModel>, message: string): void {
        alert().IsVisible().Value(true);
        alert().Message(message);
        alert().Type("Warning");
    }

    public static showWarning(alert: KnockoutObservable<AlertModel>, message: string): void {
        alert().IsVisible().Value(true);
        alert().Message(message);
        alert().Type("Danger");
    }
}

(window as any).AlertExtensions = AlertExtensions;