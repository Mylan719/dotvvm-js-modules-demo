export default moduleContext => new QrCodeLinkModule(moduleContext);

class QrCodeLinkModule
{
    private moduleContext;

    constructor(moduleContext) {

        const QRCode = (window as any).QRCode;
            
        const qrcode = new QRCode(moduleContext.elements[0], {
            text: location.href,
            width: 128,
            height: 128,
            colorDark: "#000000",
            colorLight: "#ffffff",
            correctLevel: QRCode.CorrectLevel.H
        })

        this.moduleContext = moduleContext;
    }
} 