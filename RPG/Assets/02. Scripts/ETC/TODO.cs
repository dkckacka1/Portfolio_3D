﻿//
//TODO
//=================================================
// 해야 할 것
//=================================================

//=================================================
// 버그 수정
//=================================================
// 전투 종료시 얻은 강화 티켓의 수가 엄청나지는 버그

//=================================================
// 기획안
//=================================================

// 장비는 어떤 구조를 가지고 있는가?
// 장비 : 이름, 등급(노말, 레어, 유니크, 전설), 종류(무기, 머리, 아머, 바지)
// 무기 : 공격력, 공격속도, 공격거리, 이동속도, 치명타확률, 치명타피해 증가, 적중률
// 아머 : 방어력, 체력,이동속도, 회피율
// 머리 : 방어력, 체력,치명타 확률 감소, 치명타 피해 감소
// 바지 : 방어력, 체력,이동속도

// 스킬
// 무기 (접두 : 무기 공격시 이벤트 발생, 접미 : 무기 공격 시 이벤트 발생)
// 갑옷 (접두 : 전투시 초당 이벤트 발생, 접미 : 타격받을 시 이벤트 발생)
// 투구 (접두 : 크리티컬 공격시 이벤트 발생 , 접미 : 액티브 스킬)
// 바지 (접두 : 이동시 이벤트 발생, 접미 : 액티브 스킬)

// 대상이 출혈상태면 추가 데미지 들어간다 이런거?
// 공격횟수에 맞춰서
// 내 체력에 맞추는 스테이터스 변화


//=================================================
// Class 역할 정리
//=================================================
// Status의 역할
/*
    1. Data 정리
*/

// Controller의 역할
/*
    1. AI 조건 정리
*/

// State의 역할
/*
    1. Animator 수정
    2. AI의 수행
*/

// 행동의 역할
/*
    1. 수치에 의한 정리
*/

//=================================================
// 함수 이름 정리
//=================================================
// Show...(...) : UI에서 사용한다.(정보에 맞춰서 UI를 업데이트한다.)
// SetUp(...) : 모든 게임 오브젝트에서 사용한다. (한번만 실행된다.)(컴포넌트를 연결시켜준다.)(클래스를 생성 및 초기화)
// Update...(...) : 모든 게임 오브젝트에서 사용한다. (데이터가 업데이트 되었을 때 사용한다.)
// Init(...) : 모든 게임 오브젝트에서 사용한다. (SetActive(true) 되기 전 사용한다.)
// Release...(...) : 모든 게임 오브젝트에서 사용한다. ( SetActive(false) 되기 전 호출한다.)


//=================================================
// 공모전
//=================================================

// 접두:
// 타버린, 그을린, 녹슨, 예리한, 피묻은, 더러운, 성실한 ??, 민첩한, 유능한, 초월적인, 
// 이가 빠진, 유일한, 방치된, 튼튼한, 울음의, 사나운, 울부짖는, 끈적한, 끈끈한, 조잡한, 외로운,
// 강운의, 음율의
   
// 접미 : 
// 돌덩이, 꿀벌, 악마, 운명, 늑대, 사자, 표범, 얼음, 이빨, 발톱, 화염, 불덩이, 장벽, 가호, 은총,
// 청개구리, 행운, 나락, 분노, 바보, 검객, 혼돈