/*
HanCaret
Copyright 2017, Kim Jongkook d0nzs00n@gmail.com

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and 
associated documentation files (the "Software"), to deal in the Software without restriction, 
including without limitation the rights to use, copy, modify, merge, publish, distribute, 
sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is 
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or 
substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT 
NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND 
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, 
DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
using JKCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IME_Status
{
    public class ImeStatus
    {
        /// <summary>
        /// 한/영 상태 확인
        /// </summary>
        /// <param name="hWnd">상태를 확인할 윈도우핸들</param>
        /// <param name="isHangul">리턴값 한글이면 true 영문이면 false</param>
        /// <returns>확인에 실패한 경우 false</returns>
        public static bool GetStatus(IntPtr hWnd, ref bool isHangul)
        {
            if (hWnd == IntPtr.Zero)
                return false;

            IntPtr hImeWnd = Win32.ImmGetDefaultIMEWnd(hWnd);
            if (hImeWnd == IntPtr.Zero)
                return false;

            uint IMC_GETOPENSTATUS = 5;
            //uint IMC_GETCONVERSIONMODE = 1;
            Win32.ImeConversionMode mode = (Win32.ImeConversionMode)Win32.SendMessage(hImeWnd, Win32.WindowMessage.ImeControl, (IntPtr)IMC_GETOPENSTATUS, IntPtr.Zero);

            // 일본어, 중국어 등에 대한 처리는 안되어 있으니 주의.

            isHangul = mode==Win32.ImeConversionMode.IME_CMODE_HANGUL;

            return true;
        }
    }
}
