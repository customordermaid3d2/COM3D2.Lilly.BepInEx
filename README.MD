# 뭐지 이건

- 연습용으로 만드는 플러그인
- 내가 만들어놓고 내가 모르기도 함


# 필요한거

- COM3D2.API.dll  https://github.com/JustAGuest4168/COM3D2.OutfitMatchHelper/releases
- System.Threading.dll  어지간하면 탐색기에서 발견 가능. 릴리즈에 첨부하긴 함


# 설치 위치

- COM3D2\BepInEx\plugins


# 구현해본것


## 자동 기능

- 메이드 관리 화면에서 모든 버튼 활성화(MaidManagementMainPatch)
- 메이드 에딧 진입시 모든스텟 적용 (SetMaidStatus)


## 기어 메뉴 기능

- 현제 보이는 메인 메이드 정보값 출력 (현재는 일반 플레그만)
- 게임 정보 얻기(쓸데가 있나? 분석용) 
- 라이프 모드 시나리오 클리어(모든 메이드와 플레이어 시나리오)
- 밤시중 직업? 클리어
- 모든 메이드 스텟 최대 , 특성 획득 , 스킬 획득 
- 모든 메이드 이벤트 클리어 완료 
- 현제 보이는 메인 메이드 이벤트 클리어 제거
- 모든 메이드 이벤트 클리어 제거
- 자동기능 온오프
- 로그 기록 온오프


# 컴파일 방법

- comfile.bat 파일을 본인 환경에 맞게 수정
- comfile.bat 실행하기 (obj 폴더는 삭제하도록 명령어 추가)


# 구성 

- Patch : 0Harmony20 를 이용한 플러그인만 넣어둠
- plugin : 메인 플러그인 및 BepInPlugin 관련된것만. 특히 OnSceneLoaded 관련
- Utill : 내가 쓰기 편하게...


# plugin folder


## Lilly

- 플러그인 메인 클래스
- 여기서 전부 시작


## GearMenuAddPlugin

- 기어 메뉴 추가 모음
- 클릭시 모든 메이드 스텟 및 스킬 멕스 설정


## SceneMaidManagementPlugin

- 괜히 분리한듯. 뭘 할려고 분리한거 같은데 나중에 다시 합쳐야 할듯


# utill folder


## MaidStatusUtill

- 메이드 스텟 설정 관련. 스텟 및 스킬을 전부 습듭한다던가


## MyLog

- 로그 찍는 클레스. 좀더 쉽게 못하려나


## ScenarioDataUtill

- 이벤트 관련


## ThreadPlugin

- 스레드 연습용


# Patch folder


## AudioSourceMgrPatch

- 소리 출력되는 파일명 로그찍음


## CharacterMgrPatch

- preset파일 불러올때 menu파일들 로그로 찍기


## MaidManagementMainPatch

- 메이드 관리 화면
- (제거됨)메이드 스테이터스 멕스 찍기 테스트중
- 모든 버튼 활성화


## SceneEditPatch

- 메이드 에딧 모드 클래스
- 메이드 스테이터스 멕스 찍기


## ScoutManagerPatch

- 스카웃 모드 클래스
- 스카웃 모드 버튼 뜰수 있게 메이드 제한수 제거 코드 가져와서 넣음


## ScriptManagerPatch

- ks 스크립트 관련?


## StatusMgrPatch

- 메이드 스텟 관련 클래스
- 게임 원본 코드 그대도 복사해봄
- 메이드 목록에서 스테이터스 버튼 클릭시 해당 메이드 스텟 및 스킬 습득하도록 설정

