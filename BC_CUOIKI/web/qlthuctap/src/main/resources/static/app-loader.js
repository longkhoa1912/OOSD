(() => {
  'use strict';

  const viewFiles = [
    '/views/login.html',
    '/views/app-shell.html',
    '/views/dashboard.html',
    '/views/workspace.html'
  ];

  async function start() {
    const responses = await Promise.all(viewFiles.map(path => fetch(path)));
    const failed = responses.find(response => !response.ok);
    if (failed) throw new Error(`Không tải được giao diện: ${failed.url}`);
    const [login, shell, dashboard, workspace] = await Promise.all(responses.map(response => response.text()));
    const appShell = shell.replace('<!-- EDU06_PAGE_CONTENT -->', dashboard + workspace);
    document.getElementById('appRoot').innerHTML = login + appShell;

    const script = document.createElement('script');
    script.src = '/app.js?v=edu06-uc14-reports-20261115';
    script.onload = () => document.dispatchEvent(new Event('edu06:views-ready'));
    script.onerror = () => showLoadError('Không tải được app.js. Hãy kiểm tra thư mục static.');
    document.body.appendChild(script);
  }

  function showLoadError(message) {
    document.getElementById('appRoot').innerHTML = `<main style="margin:3rem auto;max-width:42rem;padding:2rem;font:16px sans-serif"><h1>Không mở được giao diện EDU-06</h1><p>${message}</p><p>Kiểm tra các file trong <code>src/main/resources/static/views</code> rồi tải lại trang.</p></main>`;
  }

  start().catch(error => showLoadError(error.message));
})();
