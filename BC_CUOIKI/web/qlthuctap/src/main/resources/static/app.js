(() => {
  'use strict';

  const roleNames = { SinhVien: 'Sinh viên', DoanhNghiep: 'Doanh nghiệp', GiangVien: 'Giảng viên', Admin: 'Quản trị viên' };
  const roleGroups = {
    SinhVien: [
      { title: 'Thực tập', items: [['uc04','Tìm doanh nghiệp','⌕'],['uc05','Vị trí thực tập','▤'],['uc06','Đăng ký thực tập','＋'],['uc07','Theo dõi đơn','◷']] },
      { title: 'Hồ sơ & tiến độ', items: [['uc03-capnhat','Hồ sơ cá nhân','♙'],['uc15','Nộp nhật ký','☷'],['uc16','Nộp báo cáo','▧'],['uc22','Đánh giá doanh nghiệp','☆']] }
    ],
    DoanhNghiep: [
      { title: 'Tuyển dụng', items: [['uc08','Quản lý vị trí','▤'],['uc09','Xét duyệt đơn','✓'],['uc10','Theo dõi sinh viên','♙'],['uc11','Đánh giá sinh viên','☆']] }
    ],
    GiangVien: [
      { title: 'Hướng dẫn thực tập', items: [['uc12','Sinh viên được phân công','♙'],['uc13','Theo dõi tiến độ','◷'],['uc14','Đánh giá báo cáo','▧']] }
    ],
    Admin: [
      { title: 'Quản trị hệ thống', items: [['uc17','Quản lý tài khoản','♙'],['uc18','Quản lý doanh nghiệp','▦'],['uc19','Quản lý giảng viên','♧'],['uc20','Quản lý đợt thực tập','◷'],['uc21','Thống kê tổng quan','▥'],['uc23','Đánh giá doanh nghiệp','☆']] }
    ]
  };

  const definitions = {
    'uc02': { title:'Đăng ký tài khoản', desc:'Tạo tài khoản và hồ sơ phù hợp với vai trò.', role:'Công khai', group:'Tài khoản', button:'Tạo tài khoản', fields:[
      f('TenDangNhap','Tên đăng nhập','text',true), f('MatKhauHash','Mật khẩu','password',true), f('VaiTro','Vai trò','select',true,[['SinhVien','Sinh viên'],['GiangVien','Giảng viên'],['DoanhNghiep','Doanh nghiệp']]), f('Email','Email','email'), f('MaSoSinhVien','Mã số sinh viên'), f('MaSoGiangVien','Mã số giảng viên'), f('HoTen','Họ và tên'), f('Lop','Lớp'), f('Nganh','Ngành / khoa'), f('TenDoanhNghiep','Tên doanh nghiệp'), f('LinhVuc','Lĩnh vực'), f('DiaChi','Địa chỉ','text'), f('SoDienThoai','Số điện thoại') ] },
    'uc03-capnhat': { title:'Hồ sơ cá nhân', desc:'Xem thông tin đã lưu, cập nhật hồ sơ và email.', group:'Hồ sơ & tiến độ', button:'Lưu thay đổi', fields:[f('HoTen','Họ và tên','text',true),f('Lop','Lớp'),f('Nganh','Ngành'),f('SoDienThoai','Số điện thoại'),f('DiaChi','Địa chỉ','textarea'),f('Email','Email','email')] },
    'uc04': { title:'Tìm kiếm doanh nghiệp', desc:'Tìm theo tên doanh nghiệp, lĩnh vực hoặc địa chỉ. Để trống từ khóa để xem tất cả.', group:'Thực tập', button:'Tìm kiếm', auto:true, fields:[f('TuKhoa','Từ khóa','text',false,'','Ví dụ: phần mềm')] },
    'uc05': { title:'Danh sách vị trí thực tập', desc:'Lọc vị trí theo từ khóa, đợt thực tập và trạng thái tuyển.', group:'Thực tập', button:'Tìm vị trí', auto:true, fields:[f('TuKhoa','Từ khóa'),f('MaDot','Mã đợt','number'),f('ChiDangTuyen','Trạng thái tuyển','select',false,[['1','Đang tuyển'],['0','Tất cả trạng thái']]) ] },
    'uc06': { title:'Đăng ký thực tập', desc:'Chọn vị trí, viết thư giới thiệu và gửi đơn ứng tuyển.', group:'Thực tập', button:'Gửi đơn đăng ký', fields:[f('MaViTri','Vị trí thực tập','position',true),f('ThuGioiThieu','Thư giới thiệu','textarea')] },
    'uc07': { title:'Theo dõi đơn thực tập', desc:'Danh sách đơn đăng ký của tài khoản sinh viên đang đăng nhập.', group:'Thực tập', button:'Tải lại danh sách', auto:true, fields:[] },
    'uc08': { title:'Quản lý vị trí thực tập', desc:'Tạo, cập nhật, ngừng tuyển hoặc mở lại vị trí của doanh nghiệp.', group:'Tuyển dụng', button:'Thực hiện thao tác', auto:true, fields:[f('HanhDong','Thao tác','select',true,[['DANHSACH','Xem danh sách'],['TAO','Tạo vị trí'],['CAPNHAT','Cập nhật'],['NGUNG_TUYEN','Ngừng tuyển'],['MO_TUYEN','Mở tuyển']]),f('MaViTri','Mã vị trí','number'),f('MaDot','Mã đợt','number'),f('TenViTri','Tên vị trí'),f('SoLuong','Số lượng tuyển','number'),f('DiaDiem','Địa điểm'),f('MoTa','Mô tả','textarea'),f('YeuCau','Yêu cầu','textarea')] },
    'uc09': { title:'Xét duyệt đơn đăng ký', desc:'Chọn đơn chờ duyệt, chọn kết quả và ghi chú.', group:'Tuyển dụng', button:'Gửi kết quả xét duyệt', auto:true, fields:[f('MaDon','Mã đơn','number',true),f('TrangThaiDon','Kết quả','select',true,[['DaDuyet','Duyệt đơn'],['TuChoi','Từ chối']]),f('GhiChu','Ghi chú','textarea')] },
    'uc10': { title:'Theo dõi sinh viên thực tập', desc:'Xem sinh viên đang thực tập tại doanh nghiệp của bạn.', group:'Tuyển dụng', button:'Tải lại danh sách', auto:true, fields:[] },
    'uc11': { title:'Đánh giá sinh viên', desc:'Chấm điểm sinh viên trong hồ sơ thực tập thuộc doanh nghiệp.', group:'Tuyển dụng', button:'Lưu đánh giá', fields:[f('MaThucTap','Mã hồ sơ thực tập','number',true),f('Diem','Điểm (0–10)','number',true,'','0 đến 10'),f('NhanXet','Nhận xét','textarea')] },
    'uc12': { title:'Sinh viên được phân công', desc:'Danh sách hồ sơ thực tập được phân công cho giảng viên.', group:'Hướng dẫn thực tập', button:'Tải lại danh sách', auto:true, fields:[] },
    'uc13': { title:'Theo dõi tiến độ', desc:'Xem nhật ký, báo cáo và hoạt động của hồ sơ được phân công.', group:'Hướng dẫn thực tập', button:'Tra cứu tiến độ', auto:true, fields:[f('MaThucTap','Mã hồ sơ (để trống xem tất cả)','number')] },
    'uc14': { title:'Đánh giá báo cáo', desc:'Chọn báo cáo chưa chấm của sinh viên được phân công.', group:'Hướng dẫn thực tập', button:'Lưu điểm báo cáo', auto:true, fields:[f('MaBaoCao','Mã báo cáo','number',true),f('Diem','Điểm (0–10)','number',true),f('NhanXet','Nhận xét','textarea')] },
    'uc15': { title:'Nộp nhật ký thực tập', desc:'Ghi nhật ký theo ngày trong thời gian thực tập.', group:'Hồ sơ & tiến độ', button:'Nộp nhật ký', fields:[f('MaThucTap','Mã hồ sơ thực tập','number',true),f('NgayGhi','Ngày ghi','datetime-local',true),f('NoiDung','Nội dung nhật ký','textarea',true)] },
    'uc16': { title:'Nộp báo cáo', desc:'Nộp nội dung báo cáo hoặc đường dẫn tệp.', group:'Hồ sơ & tiến độ', button:'Nộp báo cáo', fields:[f('MaThucTap','Mã hồ sơ thực tập','number',true),f('TieuDe','Tiêu đề','text',true),f('NoiDung','Nội dung','textarea'),f('DuongDanTep','Đường dẫn tệp')] },
    'uc17': { title:'Quản lý tài khoản', desc:'Chọn một dòng để cập nhật, khóa/mở khóa hoặc ngừng hoạt động tài khoản.', group:'Quản trị hệ thống', button:'Thực hiện thao tác', auto:true, fields:[f('HanhDong','Thao tác','select',true,[['DANHSACH','Tải danh sách'],['TAO','Tạo tài khoản'],['CAPNHAT','Cập nhật'],['KHOA','Khóa tài khoản'],['MOKHOA','Mở khóa tài khoản'],['XOA','Xóa / ngừng hoạt động']]),f('MaTK','Mã tài khoản','number'),f('TenDangNhap','Tên đăng nhập'),f('MatKhauHash','Mật khẩu mới','password'),f('VaiTro','Vai trò','select',false,[['Admin','Admin'],['SinhVien','Sinh viên'],['GiangVien','Giảng viên'],['DoanhNghiep','Doanh nghiệp']]),f('Email','Email','email')] },
    'uc18': { title:'Quản lý doanh nghiệp', desc:'Chọn doanh nghiệp để cập nhật hoặc ngừng hoạt động. Tạo mới cần mã tài khoản doanh nghiệp.', group:'Quản trị hệ thống', button:'Thực hiện thao tác', auto:true, fields:[f('HanhDong','Thao tác','select',true,[['DANHSACH','Tải danh sách'],['TAO','Tạo mới'],['CAPNHAT','Cập nhật'],['KHOA','Khóa'],['MOKHOA','Mở khóa'],['XOA','Xóa / ngừng hoạt động']]),f('MaDN','Mã doanh nghiệp','number'),f('MaTK','Mã tài khoản','number'),f('MaSoThue','Mã số thuế'),f('TenDoanhNghiep','Tên doanh nghiệp'),f('LinhVuc','Lĩnh vực'),f('DiaChi','Địa chỉ'),f('SoDienThoai','Số điện thoại'),f('Website','Website'),f('MoTa','Mô tả','textarea')] },
    'uc19': { title:'Quản lý giảng viên', desc:'Chọn giảng viên để cập nhật hoặc ngừng hoạt động.', group:'Quản trị hệ thống', button:'Thực hiện thao tác', auto:true, fields:[f('HanhDong','Thao tác','select',true,[['DANHSACH','Tải danh sách'],['TAO','Tạo mới'],['CAPNHAT','Cập nhật'],['KHOA','Khóa'],['MOKHOA','Mở khóa'],['XOA','Xóa / ngừng hoạt động']]),f('MaGV','Mã giảng viên','number'),f('MaTK','Mã tài khoản','number'),f('MaSoGiangVien','Mã số giảng viên'),f('HoTen','Họ và tên'),f('Khoa','Khoa'),f('SoDienThoai','Số điện thoại')] },
    'uc20': { title:'Quản lý đợt thực tập', desc:'Tạo, cập nhật, đổi trạng thái hoặc xóa đợt chưa có vị trí liên quan.', group:'Quản trị hệ thống', button:'Thực hiện thao tác', auto:true, fields:[f('HanhDong','Thao tác','select',true,[['DANHSACH','Tải danh sách'],['TAO','Tạo đợt'],['CAPNHAT','Cập nhật'],['DOITRANGTHAI','Đổi trạng thái'],['XOA','Xóa đợt']]),f('MaDot','Mã đợt','number'),f('TenDot','Tên đợt'),f('NgayBatDau','Ngày bắt đầu','datetime-local'),f('NgayKetThuc','Ngày kết thúc','datetime-local'),f('HanDangKy','Hạn đăng ký','datetime-local'),f('TrangThai','Trạng thái','select',false,[['ChuaMo','Chưa mở'],['DangMo','Đang mở'],['DaDong','Đã đóng']]),f('MoTa','Mô tả','textarea')] },
    'uc21': { title:'Thống kê tổng quan', desc:'Thống kê vị trí đang tuyển, đơn đăng ký, đơn được duyệt và báo cáo theo đợt.', group:'Quản trị hệ thống', button:'Xem thống kê', auto:true, fields:[f('MaDot','Mã đợt (để trống xem tất cả)','number')] },
    'uc22': { title:'Đánh giá doanh nghiệp', desc:'Sinh viên đánh giá doanh nghiệp tại hồ sơ thực tập của mình.', group:'Hồ sơ & tiến độ', button:'Gửi đánh giá', fields:[f('MaThucTap','Mã hồ sơ thực tập','number',true),f('Diem','Điểm (0–10)','number',true),f('NhanXet','Nhận xét','textarea')] },
    'uc23': { title:'Quản lý đánh giá doanh nghiệp', desc:'Lọc đánh giá, chọn dòng để cập nhật hoặc xóa.', group:'Quản trị hệ thống', button:'Thực hiện thao tác', auto:true, fields:[f('HanhDong','Thao tác','select',true,[['DANHSACH','Tải danh sách'],['CAPNHAT','Cập nhật đánh giá'],['XOA','Xóa đánh giá']]),f('MaDGDN','Mã đánh giá','number'),f('Diem','Điểm (0–10)','number'),f('NhanXet','Nhận xét','textarea'),f('TuKhoa','Lọc theo doanh nghiệp/sinh viên')] }
  };

  function f(key, label, type='text', required=false, options=null, placeholder='') { return { key, label, type, required, options, placeholder }; }
  const $ = id => document.getElementById(id);
  let currentUser = null;
  let currentRoute = null;
  let lastRows = [];
  let toastTimer = null;

  function valueOf(object, ...keys) { for (const key of keys) if (object && object[key] !== undefined) return object[key]; return null; }
  function escapeHtml(value) { return String(value ?? '').replace(/[&<>"']/g, c => ({'&':'&amp;','<':'&lt;','>':'&gt;','"':'&quot;',"'":'&#39;'}[c])); }
  function niceColumn(key) {
    const names = {MaTK:'Mã tài khoản',TenDangNhap:'Tên đăng nhập',VaiTro:'Vai trò',Email:'Email',MaSV:'Mã sinh viên',MSSV:'MSSV',MaSoSinhVien:'MSSV',HoTen:'Họ tên',HoTenSinhVien:'Sinh viên',MaDN:'Mã doanh nghiệp',TenDoanhNghiep:'Doanh nghiệp',LinhVuc:'Lĩnh vực',MaViTri:'Mã vị trí',MaVT:'Mã vị trí',TenViTri:'Vị trí',MaDot:'Mã đợt',TenDot:'Đợt thực tập',SoLuong:'Chỉ tiêu',DiaDiem:'Địa điểm',TrangThai:'Trạng thái',TrangThaiDon:'Trạng thái đơn',MaDon:'Mã đơn',NgayNop:'Ngày nộp',ThuGioiThieu:'Thư giới thiệu',GhiChu:'Ghi chú',MaThucTap:'Mã hồ sơ',MaTT:'Mã hồ sơ',MaGV:'Mã giảng viên',HoTenGiangVien:'Giảng viên',NgayBatDau:'Bắt đầu',NgayKetThuc:'Kết thúc',MaBaoCao:'Mã báo cáo',MaBC:'Mã báo cáo',TieuDe:'Tiêu đề',DuongDanTep:'Tệp',Diem:'Điểm',NhanXet:'Nhận xét',NgayDanhGia:'Ngày đánh giá',MaDGDN:'Mã đánh giá',TongViTriDangTuyen:'Vị trí đang tuyển',TongDonGui:'Tổng đơn',SoDonDaDuyet:'Đơn đã duyệt',SoBaoCaoDaNop:'Báo cáo đã nộp',SoNhatKy:'Số nhật ký',SoBaoCao:'Số báo cáo',NgayCapNhatNhatKy:'Nhật ký gần nhất',DangHoatDong:'Đang hoạt động',NgayTao:'Ngày tạo',NgayCapNhat:'Ngày cập nhật',MaSoThue:'Mã số thuế',MaSoGiangVien:'Mã số GV',Khoa:'Khoa',Website:'Website',MoTa:'Mô tả',YeuCau:'Yêu cầu',HanDangKy:'Hạn đăng ký',NgayGhi:'Ngày ghi'};
    return names[key] || key;
  }

  async function api(url, options={}) {
    const response = await fetch(url, { headers: { 'Accept':'application/json', ...(options.body ? {'Content-Type':'application/json'} : {}), ...(options.headers||{}) }, ...options });
    let payload;
    try { payload = await response.json(); } catch (_) { throw new Error(`Backend trả phản hồi không phải JSON (HTTP ${response.status}).`); }
    if (!response.ok || payload.success === false) throw new Error(payload.message || `Yêu cầu thất bại (HTTP ${response.status}).`);
    return payload;
  }

  function showLogin(message='') {
    $('appView').classList.add('hidden');
    $('loginView').classList.remove('hidden');
    if ($('loginPanel')) $('loginPanel').classList.add('hidden');
    $('loginMessage').textContent = message;
  }

  function showApp() {
    $('loginView').classList.add('hidden');
    if ($('loginPanel')) $('loginPanel').classList.add('hidden');
    $('appView').classList.remove('hidden');
    const role = currentUser.vaiTro;
    const displayName = currentUser.tenDangNhap || currentUser.TenDangNhap || 'Người dùng';
    $('userName').textContent = displayName;
    $('userRole').textContent = roleNames[role] || role;
    $('userAvatar').textContent = displayName.slice(0,1).toUpperCase();
    $('welcomeTitle').textContent = `Xin chào, ${displayName}`;
    $('todayDate').textContent = new Intl.DateTimeFormat('vi-VN',{weekday:'long',day:'2-digit',month:'long',year:'numeric'}).format(new Date());
    renderNav();
    renderQuickCards();
    goHome();
  }

  function renderNav() {
    const groups = roleGroups[currentUser.vaiTro] || [];
    $('mainNav').innerHTML = groups.map(group => `<div class="nav-group"><div class="nav-group-title">${escapeHtml(group.title)}</div>${group.items.map(([route,title,icon])=>`<button class="nav-item" data-route="${route}"><span class="nav-icon">${icon}</span><span>${escapeHtml(title)}</span></button>`).join('')}</div>`).join('');
    $('mainNav').querySelectorAll('[data-route]').forEach(button => button.addEventListener('click', () => openRoute(button.dataset.route)));
  }

  function renderQuickCards() {
    const items = (roleGroups[currentUser.vaiTro] || []).flatMap(group => group.items).slice(0,4);
    $('quickGrid').innerHTML = items.map(([route,title,icon]) => `<button class="quick-card" data-route="${route}"><span class="q-icon">${icon}</span><b>${escapeHtml(title)}</b><small>Mở chức năng →</small></button>`).join('');
    $('quickGrid').querySelectorAll('[data-route]').forEach(button => button.addEventListener('click', () => openRoute(button.dataset.route)));
  }

  function goHome() {
    currentRoute = null;
    $('homePage').classList.remove('hidden'); $('workPage').classList.add('hidden');
    $('crumbCurrent').textContent = 'Tổng quan';
    document.querySelectorAll('.nav-item').forEach(n=>n.classList.remove('active'));
  }

  function openRoute(route) {
    const config = definitions[route];
    if (!config) return;
    if (route !== 'uc02') {
      const allowed = (roleGroups[currentUser.vaiTro] || []).some(group => group.items.some(item => item[0] === route));
      if (!allowed) return toast('Vai trò hiện tại không được phép mở chức năng này.', true);
    }
    currentRoute = route;
    lastRows = [];
    $('homePage').classList.add('hidden'); $('workPage').classList.remove('hidden');
    $('workTitle').textContent = config.title;
    $('workDescription').textContent = config.desc;
    $('workKicker').textContent = config.group || 'TÀI KHOẢN';
    $('crumbCurrent').textContent = config.title;
    $('resultsTitle').textContent = config.title;
    $('workMessage').className = 'notice hidden';
    $('mainNav').querySelectorAll('.nav-item').forEach(n=>n.classList.toggle('active', n.dataset.route === route));
    $('sidebar').classList.remove('open');
    renderForm(config);
    $('tableWrap').innerHTML = emptyMarkup('Đang tải dữ liệu','Đợi một chút trong khi tải thông tin.');
    if (route === 'uc03-capnhat') loadProfile();
    else if (route === 'uc06') loadPositionsForApply();
    else if (config.auto) executeRoute(true);
  }

  function renderForm(config) {
    const form = $('workForm');
    const confirmField = currentRoute === 'uc02' ? `<div class="field"><label for="confirmPassword">Nhập lại mật khẩu <span style="color:#d44747">*</span></label><input id="confirmPassword" type="password" required autocomplete="new-password"></div>` : '';
    form.innerHTML = `<div class="form-grid">${config.fields.map(fieldMarkup).join('')}${confirmField}</div><div class="form-actions"><button type="reset" class="btn quiet" id="clearForm">Xóa nội dung</button><button type="submit" class="btn primary" id="submitAction">${escapeHtml(config.button)}</button></div>`;
    form.onsubmit = async event => { event.preventDefault(); await executeRoute(false); };
    $('clearForm').addEventListener('click', () => setTimeout(()=>{
      if(currentRoute==='uc09') setInput('TrangThaiDon','DaDuyet');
      if(currentRoute==='uc02') updateRegistrationFields();
    },0));
    if (currentRoute === 'uc02') {
      $('field-VaiTro').addEventListener('change', updateRegistrationFields);
      updateRegistrationFields();
    }
    if (routeManagement(routeName())) {
      form.addEventListener('change', event => { if(event.target && event.target.name==='HanhDong') updateActionHint(event.target.value); });
    }
  }

  function routeName() { return currentRoute; }
  function routeManagement(route) { return ['uc08','uc09','uc17','uc18','uc19','uc20','uc23'].includes(route); }

  function fieldMarkup(field) {
    const common = `name="${escapeHtml(field.key)}" id="field-${escapeHtml(field.key)}" ${field.required?'required':''} ${field.placeholder?`placeholder="${escapeHtml(field.placeholder)}"`:''}`;
    let control;
    if (field.type === 'textarea') control = `<textarea ${common} rows="3"></textarea>`;
    else if (field.type === 'select') control = `<select ${common}>${(field.options||[]).map(([value,label])=>`<option value="${escapeHtml(value)}">${escapeHtml(label)}</option>`).join('')}</select>`;
    else if (field.type === 'position') control = `<select ${common}><option value="">Đang tải vị trí…</option></select>`;
    else control = `<input ${common} type="${escapeHtml(field.type||'text')}" ${field.type==='number'?'step="any"':''}>`;
    return `<div class="field"><label for="field-${escapeHtml(field.key)}">${escapeHtml(field.label)}${field.required?' <span style="color:#d44747">*</span>':''}</label>${control}</div>`;
  }

  async function loadProfile() {
    try {
      const response = await postUseCase('uc03-xem', { MaTK: currentUser.maTK });
      const profile = Array.isArray(response.data) ? response.data[0] : null;
      if (!profile) throw new Error('Tài khoản chưa có hồ sơ sinh viên.');
      for (const [key,value] of Object.entries(profile)) setInput(key, value);
      setNotice('Đã tải hồ sơ. Bạn có thể chỉnh sửa rồi lưu lại.', 'success');
      renderRows(response.data);
    } catch(error) { setNotice(error.message, 'error'); renderRows([]); }
  }

  async function loadPositionsForApply() {
    const select = $('field-MaViTri');
    try {
      const response = await api('/api/vitri/danh-sach');
      const positions = (response.data||[]).filter(item => item.TrangThai === 'DangTuyen');
      select.innerHTML = '<option value="">Chọn vị trí thực tập</option>' + positions.map(item=>`<option value="${item.MaVT}">${escapeHtml(item.TenViTri)} — ${escapeHtml(item.TenDoanhNghiep)} (${escapeHtml(item.DiaDiem)})</option>`).join('');
      if (!positions.length) setNotice('Hiện chưa có vị trí đang tuyển.', 'info');
    } catch(error) { select.innerHTML='<option value="">Không tải được vị trí</option>'; setNotice(error.message,'error'); }
  }

  async function executeRoute(isAuto) {
    const form = $('workForm');
    if (!isAuto && !form.reportValidity()) return;
    const route = currentRoute;
    const config = definitions[route];
    const action = form.elements.HanhDong ? form.elements.HanhDong.value : null;
    if (action === 'XOA' && !window.confirm('Xác nhận xóa hoặc ngừng hoạt động bản ghi đã chọn?')) return;
    const payload = collectForm(route);
    if (route === 'uc09' && !payload.MaDon) return setNotice('Hãy chọn một đơn trong bảng trước.', 'error');
    try {
      $('submitAction').disabled = true;
      $('submitAction').textContent = 'Đang xử lý…';
      let response;
      if (route === 'uc09' && (isAuto || action === 'DANHSACH')) response = await getPendingApplications();
      else if (route === 'uc14' && isAuto) response = await getAssignedReports();
      else response = await postUseCase(route, payload);
      if (route === 'uc02' && response.success) {
        setNotice('Tạo tài khoản thành công. Bạn có thể đăng nhập.', 'success');
        toast(response.message || 'Tạo tài khoản thành công.');
      } else {
        const rows = response.data;
        renderRows(Array.isArray(rows) ? rows : rows ? [rows] : []);
        setNotice((response.message || 'Đã xử lý thành công.') + (Array.isArray(rows) && !rows.length ? ' Không có dữ liệu phù hợp.' : ''), 'success');
        if (routeManagement(route) && action && action !== 'DANHSACH' && response.success) {
          const actionControl = form.elements.HanhDong;
          if (actionControl) actionControl.value = 'DANHSACH';
          if (route === 'uc09') {
            const refreshed = await getPendingApplications();
            renderRows(refreshed.data || []);
          } else if (['uc08','uc17','uc18','uc19','uc20','uc23'].includes(route)) {
            payload.HanhDong = 'DANHSACH';
            const refreshed = await postUseCase(route, payload);
            renderRows(refreshed.data || []);
          }
        }
        if (route === 'uc09' && !isAuto && response.success) {
          const refreshed = await getPendingApplications();
          renderRows(refreshed.data || []);
        }
        if (route === 'uc14' && !isAuto && response.success) {
          const refreshed = await getAssignedReports();
          renderRows(refreshed.data || []);
        }
        if (route === 'uc02' && response.success) form.reset();
      }
      if (isAuto && (!response.data || !response.data.length)) $('resultsTitle').textContent = config.title;
    } catch(error) {
      setNotice(error.message, 'error');
      if (!isAuto) toast(error.message, true);
    } finally {
      const button = $('submitAction');
      if (button) { button.disabled = false; button.textContent = config.button; }
    }
  }

  function collectForm(route) {
    const config = definitions[route];
    const payload = {};
    for (const field of config.fields) {
      const control = $('field-'+field.key);
      if (!control) continue;
      let value = control.value;
      if (value === '') value = null;
      if (value !== null && field.type === 'number') {
        value = Number(value);
        if (!Number.isFinite(value)) throw new Error(`${field.label} phải là số hợp lệ.`);
      }
      if (value !== null && field.key === 'ChiDangTuyen') value = value === '1';
      payload[field.key] = value;
    }
    if (route === 'uc02' && payload.MatKhauHash) {
      const confirm = $('confirmPassword');
      if (confirm && payload.MatKhauHash !== confirm.value) throw new Error('Mật khẩu nhập lại chưa khớp.');
    }
    const identity = {
      'uc03-capnhat': { MaTK: currentUser.maTK },
      uc06: { MaTKSinhVien: currentUser.maTK }, uc07: { MaTKSinhVien: currentUser.maTK },
      uc08: { MaTKDoanhNghiep: currentUser.maTK }, uc09: { MaTKDoanhNghiep: currentUser.maTK }, uc10: { MaTKDoanhNghiep: currentUser.maTK },
      uc11: { MaTKDoanhNghiep: currentUser.maTK }, uc12: { MaTKGiangVien: currentUser.maTK }, uc13: { MaTKGiangVien: currentUser.maTK },
      uc14: { MaTKGiangVien: currentUser.maTK }, uc15: { MaTKSinhVien: currentUser.maTK }, uc16: { MaTKSinhVien: currentUser.maTK },
      uc17: { MaTKAdmin: currentUser.maTK }, uc18: { MaTKAdmin: currentUser.maTK }, uc19: { MaTKAdmin: currentUser.maTK },
      uc20: { MaTKAdmin: currentUser.maTK }, uc21: { MaTKAdmin: currentUser.maTK }, uc22: { MaTKSinhVien: currentUser.maTK }, uc23: { MaTKAdmin: currentUser.maTK }
    };
    return { ...(identity[route]||{}), ...payload };
  }

  function postUseCase(route, body) { return api('/api/uc/'+encodeURIComponent(route), { method:'POST', body:JSON.stringify(body) }); }
  function getPendingApplications() { return api('/api/dondangky/danh-sach?maTKDoanhNghiep='+encodeURIComponent(currentUser.maTK)); }
  function getAssignedReports() { return api('/api/baocao/danh-sach?maTKGiangVien='+encodeURIComponent(currentUser.maTK)); }

  function setInput(key, value) {
    const control = $('field-'+key);
    if (!control || control.tagName === 'SELECT' && !Array.from(control.options).some(option => option.value === String(value??''))) return;
    if (control.type === 'datetime-local' && value) {
      const date = new Date(value);
      control.value = Number.isNaN(date.getTime()) ? String(value).slice(0,16) : new Date(date.getTime()-date.getTimezoneOffset()*60000).toISOString().slice(0,16);
    } else control.value = value == null ? '' : String(value);
  }

  function renderRows(rows) {
    lastRows = rows || [];
    $('resultCount').textContent = `${lastRows.length} bản ghi`;
    if (!lastRows.length) { $('tableWrap').innerHTML = emptyMarkup('Không có dữ liệu','Không tìm thấy bản ghi phù hợp với điều kiện hiện tại.'); return; }
    const keys = Array.from(new Set(lastRows.flatMap(row => Object.keys(row||{}))));
    $('tableWrap').innerHTML = `<table class="data-table"><thead><tr>${keys.map(key=>`<th>${escapeHtml(niceColumn(key))}</th>`).join('')}</tr></thead><tbody>${lastRows.map((row,index)=>`<tr data-row="${index}">${keys.map(key=>`<td title="${escapeHtml(displayValue(row[key]))}">${escapeHtml(displayValue(row[key]))}</td>`).join('')}</tr>`).join('')}</tbody></table>`;
    $('tableWrap').querySelectorAll('tbody tr').forEach(tr=>tr.addEventListener('click',()=>selectRow(Number(tr.dataset.row),tr)));
  }

  function selectRow(index, tr) {
    $('tableWrap').querySelectorAll('tbody tr').forEach(row=>row.classList.remove('selected'));
    tr.classList.add('selected');
    const record = lastRows[index] || {};
    for (const [key,value] of Object.entries(record)) setInput(key,value);
    if (currentRoute === 'uc09') setInput('MaDon',record.MaDon);
  }

  function displayValue(value) {
    if (value === null || value === undefined) return '—';
    if (typeof value === 'boolean') return value ? 'Có' : 'Không';
    if (typeof value === 'object') return JSON.stringify(value);
    return String(value);
  }
  function emptyMarkup(title, message) { return `<div class="empty-state"><span>▦</span><b>${escapeHtml(title)}</b><small>${escapeHtml(message)}</small></div>`; }
  function setNotice(text, type='info') {
    const notice = $('workMessage');
    // Hide generic UCxx status banners on every page; keep actionable success/error messages.
    if (type === 'success' && /^Đã xử lý\s+UC\d+/i.test(String(text || '').trim())) {
      notice.textContent = '';
      notice.className = 'notice hidden';
      return;
    }
    notice.textContent = text;
    notice.className = `notice ${type}`;
  }
  function toast(message,error=false) { const el=$('toast'); el.textContent=message; el.style.background=error?'#9d3440':'#172942'; el.classList.add('show'); clearTimeout(toastTimer); toastTimer=setTimeout(()=>el.classList.remove('show'),3500); }
  function updateActionHint(action) { if(action==='XOA') setNotice('Xóa được xác nhận thêm trước khi gửi. Một số dữ liệu sẽ được ngừng hoạt động hoặc không thể xóa nếu còn liên kết.','info'); }

  function updateRegistrationFields() {
    const role = $('field-VaiTro').value;
    const visibleByRole = {
      SinhVien: ['MaSoSinhVien','HoTen','Lop','Nganh','SoDienThoai','DiaChi'],
      GiangVien: ['MaSoGiangVien','HoTen','Nganh','SoDienThoai'],
      DoanhNghiep: ['TenDoanhNghiep','LinhVuc','DiaChi','SoDienThoai']
    }[role] || [];
    for (const key of ['MaSoSinhVien','MaSoGiangVien','HoTen','Lop','Nganh','TenDoanhNghiep','LinhVuc','DiaChi','SoDienThoai']) {
      const control = $('field-'+key);
      if (control) control.closest('.field').classList.toggle('hidden', !visibleByRole.includes(key));
    }
    const industry = document.querySelector('label[for="field-Nganh"]');
    if (industry) industry.firstChild.textContent = role === 'GiangVien' ? 'Khoa' : 'Ngành';
  }

  // The public landing page opens login in a modal instead of showing the form immediately.
  if ($('loginButton') && $('loginPanel')) {
    $('loginButton').addEventListener('click', () => {
      $('loginPanel').classList.remove('hidden');
      $('loginMessage').textContent = '';
      $('loginName').focus();
    });
    $('closeLogin')?.addEventListener('click', () => $('loginPanel').classList.add('hidden'));
    $('loginPanel').addEventListener('click', event => {
      if (event.target === $('loginPanel')) $('loginPanel').classList.add('hidden');
    });
    document.addEventListener('keydown', event => {
      if (event.key === 'Escape') $('loginPanel').classList.add('hidden');
    });
  }

  $('loginForm').addEventListener('submit', async event => {
    event.preventDefault();
    const button = event.currentTarget.querySelector('button[type="submit"]');
    button.disabled = true; button.textContent = 'Đang đăng nhập…'; $('loginMessage').textContent='';
    try {
      const response = await api('/api/auth/login',{method:'POST',body:JSON.stringify({TenDangNhap:$('loginName').value.trim(),MatKhauHash:$('loginPassword').value})});
      const data=response.data||{};
      currentUser={ maTK:valueOf(data,'MaTK','maTK'),tenDangNhap:valueOf(data,'TenDangNhap','tenDangNhap'),vaiTro:valueOf(data,'VaiTro','vaiTro'),email:valueOf(data,'Email','email') };
      if (!currentUser.maTK || !currentUser.vaiTro) throw new Error('Backend không trả đủ thông tin phiên đăng nhập.');
      sessionStorage.setItem('edu06-session',JSON.stringify(currentUser));
      showApp();
    } catch(error) { $('loginMessage').textContent=error.message; }
    finally { button.disabled=false; button.innerHTML='Đăng nhập <span aria-hidden="true">→</span>'; }
  });

  $('logoutBtn').addEventListener('click',()=>{ sessionStorage.removeItem('edu06-session'); currentUser=null; showLogin(); });
  $('registerButton').addEventListener('click',()=>{
    currentUser={maTK:null,tenDangNhap:'Khách',vaiTro:'Guest',email:null};
    sessionStorage.removeItem('edu06-session');
    showApp();
    openRoute('uc02');
  });
  $('backHome').addEventListener('click',goHome);
  $('refreshBtn').addEventListener('click',()=>{ if(currentRoute) executeRoute(true); });
  $('menuToggle').addEventListener('click',()=>$('sidebar').classList.toggle('open'));
  document.addEventListener('click',event=>{ if(window.innerWidth<=760 && $('sidebar').classList.contains('open') && !event.target.closest('#sidebar') && !event.target.closest('#menuToggle')) $('sidebar').classList.remove('open'); });

  const saved=sessionStorage.getItem('edu06-session');
  if(saved) { try { currentUser=JSON.parse(saved); showApp(); } catch(_) { sessionStorage.removeItem('edu06-session'); showLogin(); } }
  else showLogin();
})();
