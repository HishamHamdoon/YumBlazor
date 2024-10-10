using Microsoft.JSInterop;

namespace YumBlazor.Services.Extensions
{
    public static class IJSRunTimeExtension
    {
        public static async Task ToastrSuccess(this IJSRuntime jSRuntime, string message)
        {
            await jSRuntime.InvokeVoidAsync("showToaster", "success", message);
        }

        public static async Task ToastrError(this IJSRuntime jSRuntime, string message)
        {
            await jSRuntime.InvokeVoidAsync("showToaster", "error", message);
        }
        //public static async Task ToastrSuccess(this IJSRuntime js,string message)
        //{

        //    await js.InvokeVoidAsync("showToastr", "success", message);
        //}
        //public static async Task ToastrError(this IJSRuntime js, string message)
        //{
        //    await js.InvokeVoidAsync("showToastr", "error",message);
        //}
    }
}
