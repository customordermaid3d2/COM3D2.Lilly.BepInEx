# 뭐지 이건

- 연습용으로 만드는 플러그인


# 구현해본것

- 메이드 관리 화면에서 모든 버튼 활성화
- 기어메뉴에 모든 메이드 이벤트 클리어 처리 기능 추가
- 기어메뉴에 모든 메이드 스텟 최대 및 특성 획득 추가
- 기어메뉴에 아이콘(기능 없음) 추가


# 컴파일 방법

- comfile.bat 파일을 본인 환경에 맞게 수정
- obj 폴더는 삭제하고 comfile.bat 실행하기 (못없애나)


# 구성 

- Patch 폴더에는 0Harmony20 를 이용한 플러그인만 넣어둠


# plugin folder


## Lilly

- 플러그인 메인 클래스
- 여기서 전부 시작


## GearMenuAddPlugin

- 기어 메뉴 추가
- 클릭시 모든 메이드 스텟 및 스킬 멕스 설정


# utill folder


## GearMenu

- 기어메뉴 추가해주는 클래스


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

