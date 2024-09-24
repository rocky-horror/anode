#include <Windows.h>
#include <stdio.h>

typedef struct
{
	DWORD dwInterpreterLength;
	LPVOID lpInterpreterData;
	DWORD dwProgramLength;
	LPVOID lpProgramData;

	DWORD dwSizeOfExecutionInformation;
} EXECUTIONINFORMATION, *LPEXECUTIONINFORMATION;

LPEXECUTIONINFORMATION EXECUTIONINFORMATION_Load()
{
	LPEXECUTIONINFORMATION lpExecutionInfo = malloc(sizeof(EXECUTIONINFORMATION));

	if (!lpExecutionInfo)
		return NULL;

	CHAR szFilename[MAX_PATH];
	RtlZeroMemory(szFilename, MAX_PATH);
	(VOID)GetModuleFileNameA(NULL, szFilename, MAX_PATH);

	HANDLE hLocal = CreateFileA(szFilename, GENERIC_READ, 0, NULL, OPEN_EXISTING, 0, NULL);

	if (!hLocal)
		return NULL;

	DWORD dwSize = GetFileSize(hLocal, NULL);
	LPBYTE lpLocalData = malloc(dwSize);

	if (!lpLocalData)
		return NULL;

	DWORD dwJunk;
	(VOID)ReadFile(hLocal, lpLocalData, dwSize, &dwJunk, NULL);

	(VOID)CloseHandle(hLocal);

	DWORD j = 0;
	LPBYTE lp = &lpExecutionInfo->dwSizeOfExecutionInformation;
	for (DWORD dw = dwSize - 1; dw >= (dwSize - (1 + sizeof(DWORD))); --dw)
	{
		lp[j] = lpLocalData[dw];
		++j;
	}

	DWORD dwStartExecutionInfo = (DWORD)lpLocalData + (dwSize - lpExecutionInfo->dwSizeOfExecutionInformation);
	DWORD ptr = 0;

	memcpy(&lpExecutionInfo->dwInterpreterLength, dwStartExecutionInfo + ptr, sizeof(DWORD));
	ptr += sizeof(DWORD);

	lpExecutionInfo->lpInterpreterData = malloc(lpExecutionInfo->dwInterpreterLength);
	if (!lpExecutionInfo->lpInterpreterData)
	{
		free(lpExecutionInfo);
		return NULL;
	}

	memcpy(&lpExecutionInfo->lpInterpreterData, dwStartExecutionInfo + ptr, sizeof(lpExecutionInfo->dwInterpreterLength));
	ptr += lpExecutionInfo->dwInterpreterLength;

	memcpy(&lpExecutionInfo->dwProgramLength, dwStartExecutionInfo + ptr, sizeof(DWORD));
	ptr += sizeof(DWORD);

	lpExecutionInfo->lpProgramData = malloc(lpExecutionInfo->dwProgramLength);
	if (!lpExecutionInfo->lpProgramData)
	{
		free(lpExecutionInfo->lpInterpreterData);
		free(lpExecutionInfo);
		return NULL;
	}

	free(lpLocalData);
	return lpExecutionInfo;
}

VOID EXECUTIONINFOORMATION_Save(HANDLE hFile, LPEXECUTIONINFORMATION lp)
{
	DWORD dwJunk;
	(VOID)WriteFile(hFile, &lp->dwInterpreterLength, sizeof(DWORD), &dwJunk, NULL);
	(VOID)WriteFile(hFile, lp->lpInterpreterData, lp->dwInterpreterLength, &dwJunk, NULL);
	(VOID)WriteFile(hFile, &lp->dwProgramLength, sizeof(DWORD), &dwJunk, NULL);
	(VOID)WriteFile(hFile, lp->lpProgramData, lp->dwProgramLength, &dwJunk, NULL);
	(VOID)WriteFile(hFile, &lp->dwSizeOfExecutionInformation, sizeof(DWORD), &dwJunk, NULL);
}

INT main(INT argc, LPCSTR* argv)
{
	LPEXECUTIONINFORMATION lp = EXECUTIONINFORMATION_Load();

	if (!lp)
		return -1;

	LPCSTR szTempPath[MAX_PATH];
	RtlZeroMemory(szTempPath, MAX_PATH);
	(VOID)GetTempPathA(MAX_PATH, szTempPath);

	LPCSTR szTempRuntime[MAX_PATH];
	LPCSTR szTempProgram[MAX_PATH];
	(VOID)GetTempFileNameA(szTempPath, "CTD", 0, szTempRuntime);
	(VOID)GetTempFileNameA(szTempPath, "CTD", 0, szTempProgram);

	HANDLE hTempRuntime = CreateFileA(szTempRuntime, GENERIC_WRITE, 0, NULL, OPEN_ALWAYS, 0, NULL);
	HANDLE hTempProgram = CreateFileA(szTempProgram, GENERIC_WRITE, 0, NULL, OPEN_ALWAYS, 0, NULL);

	DWORD dwJunk;
	(VOID)WriteFile(hTempRuntime, lp->lpInterpreterData, lp->dwInterpreterLength, &dwJunk, NULL);
	(VOID)WriteFile(hTempProgram, lp->lpProgramData, lp->dwProgramLength, &dwJunk, NULL);

	(VOID)CloseHandle(hTempRuntime);
	(VOID)CloseHandle(hTempProgram);

	STARTUPINFOA cStartupInfo;
	PROCESS_INFORMATION cProcessInfo;
	RtlZeroMemory(&cStartupInfo, sizeof(STARTUPINFOA));
	RtlZeroMemory(&cProcessInfo, sizeof(PROCESS_INFORMATION));

	LPCSTR szCommandLine[MAX_PATH];
	snprintf(szCommandLine, MAX_PATH, "%s%s", szTempRuntime, szTempProgram);

	if (!CreateProcessA(NULL, szCommandLine, NULL, NULL, FALSE, 0, NULL, NULL, &cStartupInfo, &cProcessInfo))
		return -2;

	(VOID)WaitForSingleObject(cProcessInfo.hProcess, INFINITE);

	CloseHandle(cProcessInfo.hProcess);
	CloseHandle(cProcessInfo.hThread);

	(VOID)DeleteFileA(szTempRuntime);
	(VOID)DeleteFileA(szTempProgram);
}