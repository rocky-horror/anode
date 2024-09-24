// dllmain.cpp : Defines the entry point for the DLL application.
#include <Windows.h>
#include "ImageSurgery.h"
#include <stdio.h>

typedef struct
{
    DWORD dwInterpreterLength;
    LPVOID lpInterpreterData;
    DWORD dwProgramLength;
    LPVOID lpProgramData;

    DWORD dwSizeOfExecutionInformation;
} EXECUTIONINFORMATION, *LPEXECUTIONINFORMATION;

DWORD CalculateSizeOfImageWithSectionAlignment(DWORD dw, DWORD dwSectionAlignment)
{
    while (dw % dwSectionAlignment != 0)
        ++dw;

    return dw;
}

LPBYTE ReadFileIntoHeap(LPCSTR lpszPath, LPDWORD lpdwSize)
{
    HANDLE hFile = CreateFileA(lpszPath, GENERIC_READ, 0, NULL, OPEN_EXISTING, 0, NULL);

    if (!hFile)
        return NULL;

    DWORD dw;
    *lpdwSize = GetFileSize(hFile, &dw);
    LPBYTE lpData = malloc(*lpdwSize);

    ReadFile(hFile, lpData, *lpdwSize, &dw, NULL);

    return lpData;
}

#define dbgalert(x) MessageBoxA(NULL, x, "", MB_OK)

EXPORT INT CraftExecutable(LPCSTR lpszDest, LPCSTR lpszStubPath, LPCSTR lpszInterpreterPath, LPCSTR lpszProgramPath)
{
    HANDLE hFile = CreateFileA(lpszDest, GENERIC_WRITE, 0, NULL, CREATE_ALWAYS, 0, NULL);

    if (!hFile)
        return 1;

    DWORD dwStubSize;
    LPBYTE lpStubData = ReadFileIntoHeap(lpszStubPath, &dwStubSize);
    DWORD dwInterpreterSize;
    LPBYTE lpInterpreterData = ReadFileIntoHeap(lpszInterpreterPath, &dwInterpreterSize);
    DWORD dwProgramSize;
    LPBYTE lpProgramData = ReadFileIntoHeap(lpszProgramPath, &dwProgramSize);

    dbgalert("Made it past reading to head");

    if (!lpStubData || !lpInterpreterData || !lpProgramData)
        return 2;

    // Calculate execution information size
    DWORD dwInformationSize = sizeof(DWORD) + dwInterpreterSize + sizeof(DWORD) + dwProgramSize;

    dbgalert("calculated information size");

    PIMAGE_DOS_HEADER lpDOSHeader = lpStubData;
    PIMAGE_NT_HEADERS64 lpNTHeaders = (PIMAGE_NT_HEADERS64)(lpStubData + lpDOSHeader->e_lfanew);
    lpNTHeaders->OptionalHeader.SizeOfImage = CalculateSizeOfImageWithSectionAlignment(dwStubSize + dwInformationSize, lpNTHeaders->OptionalHeader.SectionAlignment);

    dbgalert("modified headers");

    DWORD dwJunk;
    WriteFile(hFile, &dwInterpreterSize, sizeof(DWORD), &dwJunk, NULL);
    WriteFile(hFile, lpInterpreterData, dwInterpreterSize, &dwJunk, NULL);
    WriteFile(hFile, &dwProgramSize, sizeof(DWORD), &dwJunk, NULL);
    WriteFile(hFile, lpProgramData, dwProgramSize, &dwJunk, NULL);

    WriteFile(hFile, &dwInformationSize, sizeof(DWORD), &dwJunk, NULL);

    dbgalert("wrote file");

    CloseHandle(hFile);

    free(lpStubData);
    free(lpInterpreterData);
    free(lpProgramData);

    return 0;
}

BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
                     )
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }
    return TRUE;
}

