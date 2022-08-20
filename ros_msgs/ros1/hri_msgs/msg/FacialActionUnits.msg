# This message the intensity of each actions unit (AU), with their confidence, for a specific face.
#
# It follows the naming convention of the  Facial Action Coding System (FACS) developed by Ekman.

Header header

# List taken from https://en.wikipedia.org/wiki/Facial_Action_Coding_System

# Main codes
uint8 NEUTRAL_FACE=0  # Neutral face 
uint8 INNER_BROW_RAISER=1  # Inner brow raiser 
uint8 OUTER_BROW_RAISER=2  # Outer brow raiser 
uint8 BROW_LOWERER=4  # Brow lowerer 
uint8 UPPER_LID_RAISER=5  # Upper lid raiser 
uint8 CHEEK_RAISER=6  # Cheek raiser 
uint8 LID_TIGHTENER=7  # Lid tightener 
uint8 LIPS_TOWARD_EACH_OTHER=8  # Lips toward each other 
uint8 NOSE_WRINKLER=9  # Nose wrinkler 
uint8 UPPER_LIP_RAISER=10  # Upper lip raiser 
uint8 NASOLABIAL_DEEPENER=11  # Nasolabial deepener 
uint8 LIP_CORNER_PULLER=12  # Lip corner puller 
uint8 SHARP_LIP_PULLER=13  # Sharp lip puller 
uint8 DIMPLER=14  # Dimpler 
uint8 LIP_CORNER_DEPRESSOR=15  # Lip corner depressor 
uint8 LOWER_LIP_DEPRESSOR=16  # Lower lip depressor 
uint8 CHIN_RAISER=17  # Chin raiser 
uint8 LIP_PUCKER=18  # Lip pucker 
uint8 TONGUE_SHOW=19  # Tongue show 
uint8 LIP_STRETCHER=20  # Lip stretcher 
uint8 NECK_TIGHTENER=21  # Neck tightener 
uint8 LIP_FUNNELER=22  # Lip funneler 
uint8 LIP_TIGHTENER=23  # Lip tightener 
uint8 LIP_PRESSOR=24  # Lip pressor 
uint8 LIPS_PART=25  # Lips part 
uint8 JAW_DROP=26  # Jaw drop 
uint8 MOUTH_STRETCH=27  # Mouth stretch 
uint8 LIP_SUCK=28  # Lip suck 

# Head movement codes
uint8 HEAD_TURN_LEFT=51  # Head turn left
uint8 HEAD_TURN_RIGHT=52  # Head turn right
uint8 HEAD_UP=53  # Head up
uint8 HEAD_DOWN=54  # Head down
uint8 HEAD_TILT_LEFT=55  # Head tilt left
uint8 HEAD_TILT_RIGHT=56  # Head tilt right
uint8 HEAD_FORWARD=57  # Head forward
uint8 HEAD_BACK=58  # Head back

# Eye movement codes
uint8 EYES_TURN_LEFT=61  # Eyes turn left
uint8 EYES_TURN_RIGHT=62  # Eyes turn right
uint8 EYES_UP=63  # Eyes up
uint8 EYES_DOWN=64  # Eyes down
uint8 WALLEYE=65  # Walleye
uint8 CROSS_EYE=66  # Cross-eye
uint8 EYES_POSITIONED_TO_LOOK_AT_OTHER_PERSON=69  # Eyes positioned to look at other person : The 4, 5, or 7, alone or in combination, occurs while the eye position is fixed on the other person in the conversation.

# Visibility codes
uint8 BROWS_AND_FOREHEAD_NOT_VISIBLE=70  # Brows and forehead not visible
uint8 EYES_NOT_VISIBLE=71  # Eyes not visible
uint8 LOWER_FACE_NOT_VISIBLE=72  # Lower face not visible
uint8 ENTIRE_FACE_NOT_VISIBLE=73  # Entire face not visible
uint8 UNSOCIABLE=74  # Unsociable

# Gross behavior codes
uint8 JAW_THRUST=29  # Jaw thrust
uint8 JAW_SIDEWAYS=30  # Jaw sideways
uint8 JAW_CLENCHER=31  # Jaw clencher : masseter
uint8 LIP_BITE=32  # [Lip] bite
uint8 CHEEK_BLOW=33  # [Cheek] blow
uint8 CHEEK_PUFF=34  # [Cheek] puff
uint8 CHEEK_SUCK=35  # [Cheek] suck
uint8 TONGUE_BULGE=36  # [Tongue] bulge
uint8 LIP_WIPE=37  # Lip wipe
uint8 NOSTRIL_DILATOR=38  # Nostril dilator : nasalis (pars alaris)
uint8 NOSTRIL_COMPRESSOR=39  # Nostril compressor : nasalis (pars transversa) and depressor septi nasi
uint8 SNIFF=40  # Sniff
uint8 LID_DROOP=41  # Lid droop : Levator palpebrae superioris (relaxation)
uint8 SLIT=42  # Slit : Orbicularis oculi muscle
uint8 EYES_CLOSED=43  # Eyes closed : Relaxation of Levator palpebrae superioris
uint8 SQUINT=44  # Squint : Corrugator supercilii and orbicularis oculi muscle
uint8 BLINK=45  # Blink : Relaxation of Levator palpebrae superioris; contraction of orbicularis oculi (pars palpebralis)
uint8 WINK=46  # Wink : orbicularis oculi
uint8 SPEECH=50  # Speech
uint8 SWALLOW=80  # Swallow
uint8 CHEWING=81  # Chewing
uint8 SHOULDER_SHRUG=82  # Shoulder shrug
uint8 HEAD_SHAKE_BACK_AND_FORTH=84  # Head shake back and forth
uint8 HEAD_NOD_UP_AND_DOWN=85  # Head nod up and down
uint8 FLASH=91  # Flash
uint8 PARTIAL_FLASH=92  # Partial flash
uint8 SHIVER_TREMBLE=97  # Shiver/tremble
uint8 FAST_UP_DOWN_LOOK=98  # Fast up-down look

float32[] FAU
float32[] intensity # an array of 99 floats, one per AU. Use the constant defined above to access one specific AU.
float32[] confidence # an array of 99 floats, one per AU. Use the constant defined above to access one specific AU.

