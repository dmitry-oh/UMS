2025/12/24

#TITLE 곡 제목
#ARTIST 아티스트명
#GENRE 장르
#BPM 120.00
#PLAYLEVEL 5
#COMMENT 곡에 대한 설명
#KEYCNT 사용 키 갯수

*백그라운드 음악과 배경화면(BGA) 파일 지정*
#WAV01 bgm.wav
#BMP00 bg.bmp

*키음 파일 지정 (01~ZZ번까지 가능)*
#WAV10 keysound1.wav
#WAV11 keysound2.wav
#WAV12 keysound3.wav
#WAV13 keysound4.wav
#WAV14 keysound5.wav
#WAV15 keysound6.wav
#WAV16 keysound7.wav


#만 데이터로 받음.
**는 주석표시

노트 배치 형식: #aaa:bb:cccccccccccccccccc
aaa = 마디 번호 (000~999)
bb = 채널 번호
cccc... = 데이터 값

채널
01~07 : 각 건반 (또는 01~05, 08은 5키)
08 : 턴테이블
09 : BPM 변경
0A : STOP (변속 정지)
0D : BGA 이미지 표시
51~57 : 롱노트 (각 건반별)
60번대: 변칙노트 (데이터값:입력 건반)

데이터값
건반행:48마디로 쪼갬
BPM변경:120,180등 BPM을 받음
BGA표시:define된 파일이름
(위에서 저장된 BMP00을 쓰면 bg.bmp가 불러와지는식)