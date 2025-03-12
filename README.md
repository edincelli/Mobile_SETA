<p align="center">
<b> Mobile Security Education, Training, and Awareness (SETA) Artifact </b>
</p align="center">
  
Security Education, Training, and Awareness (SETA) programs are designed to enhance an organization’s security posture by equipping employees with the knowledge and skills needed to recognize and address security threats, such as social engineering. However, SETA programs are often implemented using a one-size-fits-all approach that does not account for unique user characteristics (D’Arcy & Hovav, 2008) and the distinct security challenges posed by different platforms or devices (Tyagi et al., 2020). To the best of our knowledge, SETA programs specifically tailored to address mobile device security remain rare. This represents a critical gap, particularly given the growing reliance on mobile devices driven by remote work and bring-your-own-device (BYOD) trends (Patten & Harris, 2013). In both personal and professional contexts, mobile-specific threats demand specialized strategies and targeted training to ensure effective risk mitigation.

This project aims to address this gap by developing a mobile SETA (mSETA) artifact specifically designed to tackle the unique challenges of securing mobile devices. The mSETA artifact focuses on enhancing mobile security awareness and mindfulness to protect sensitive information in a mobile-first environment and addresses issues such as Smishing, app permissions, mobile-specific malware, insecure networks, romance and finance scams.

To achieve this, the mSETA artifact engages users through an interactive, game-like experience that utilizes scenario-based training (Dincelli & Chengalur-Smith, 2020). This approach not only enhances users’ awareness but also promotes mindfulness by emphasizing the distinctions between secure and insecure behaviors in a mobile context through theory-driven scenarios. Users engage with realistic mobile security challenges, including:
- Recognizing financial scam calls
- Detecting phishing attempts in SMS messages
- Configuring secure app settings
- Managing permissions effectively
- Recognizing malicious app behaviors and understanding mobile malware risks
- Securing mobile devices in public networks (e.g., mitigating rogue Wi-Fi risks)

The goal is to deepen users’ understanding of mobile-specific threats while equipping them with practical, hands-on skills to mitigate risks in a dynamic and immersive learning environment.

<p align="center">
<b> Design Science Research Approach: </b>
</p align="center">

The mSETA artifact is developed using the Design Science Research (DSR) methodology, specifically employing the Echeloned DSR (eDSR) approach (Tuunanen et al., 2024). The eDSR methodology is used to decompose the project into smaller, logically coherent, self-contained parts that address distinct research questions and development challenges. The key echelons of this project are:
-	Integrating Mindfulness in Mobile SETA
-	Gamification Strategies and Mechanisms
-	Theory-driven Scenario-Based Training for Mobile-Specific Threats
-	Measurable Behavioral Outcomes

<p align="left">
<b> Integrating Mindfulness in Mobile SETA: </b>
</p align="left">
mSETA follows a gamified mindfulness-based training approach. Mindfulness is a psychological state characterized by heightened awareness and focused attention on present experiences (Langer, 1989). Studies have shown that mindfulness techniques enhance individuals’ ability to dynamically allocate attention and improve contextual awareness, thereby increasing their ability to identify phishing emails (Jensen et al., 2017). Research further suggests that mindfulness, along with an individual’s affective state, influences their preference for systematic processing, ultimately leading to greater accuracy in phishing detection (Bera & Kim, 2025).

Mindfulness-based interventions have been widely implemented in smartphone applications for various domains, including depression relapse prevention and addiction management (Creswell, 2017). However, there is currently no dedicated mindfulness program designed to address security and privacy-related threats. To achieve this, mSETA is designed following the four presence domains outlined in the Community of Inquiry (CoI) Framework. Presence refers to being fully immersed in an experience, while mindfulness emphasizes intentional awareness without distraction. In training, mindfulness enhances presence by helping users stay engaged, process information effectively, and make informed decisions without cognitive overload. The CoI framework helps identify the essential design principles that must be followed to create an immersive learning experience that enhances both mindfulness and presence.

<p align="left">
<b> Gamification Strategies and Mechanisms: </b>
</p align="left">
Integrating gamification into mindfulness-based SETA programs could enhance engagement and retention by leveraging interactive challenges, feedback mechanisms, and reward systems (Lee at al., 2025). Building on this, mSETA aims to provide a mindfulness-based intervention to immerse users in realistic security and privacy-related threat scenarios and encourage sustained attention and awareness. 

Mobile SETA is developed using the Unity game development engine. Unity is selected for its robust development capabilities, scalability, and cross-platform support. Unity’s advanced 3D and 2D frameworks enable the creation of immersive and realistic training scenarios. Unity also facilitates the integration of gamified features to further improve user engagement and support diverse experimental conditions. Unity’s modular architecture making it easy to implement future updates and add new training modules as needed. Additionally, Unity allows deployment across multiple platforms, including iOS, Android, web, and desktop, ensuring that Mobile SETA is accessible to a wide audience

<img src="https://dincelli.com/extras/mseta1.webp" height=400 align="center"/>

<p align="left">
<b> Theory-driven Scenario-Based Training for Mobile-Specific Threats: </b>
</p align="left">
What sets mSETA apart from traditional internet- and smartphone-based mindfulness programs is its use of realistic simulations that closely replicate real-world mobile-based social engineering scenarios. Unlike passive SETA methods, mSETA guides users through each stage of an attack, allowing them to actively experience the unfolding events and understand the consequences of their decisions. We follow a theory-driven approach to immerse users in simulated security threats and apply theoretical knowledge in practical settings.

For example, to reinforce user engagement and encourage mindfulness about mobile-specific threats, we implemented a consequence system inspired by Prospect Theory, which suggests that individuals are more motivated to avoid losses than to seek equivalent gains (Kim et al., 2016). We applied this principle through scenario-based training that leverages loss aversion to keep users focused and encourage proactive security behaviors. Each poor security decision results in progressively severe consequences, visually represented on a dedicated screen, allowing users to clearly see the cumulative impact of their choices. This approach strengthens mindfulness and motivates users to adopt more secure behaviors to prevent escalating losses.

<img src="https://dincelli.com/extras/mseta2.webp" height=400 align="center"/>

<p align="left">
<b> Measurable Behavioral Outcomes: </b>
</p align="left">
TBA

**References:**
- Bera, D., & Kim, D. J. (2025). The nexus of mindfulness, affect, and information processing in phishing identification: An empirical examination. Information & Management, 62(3), 104110.
- Creswell, J. D. (2017). Mindfulness interventions. Annual Review of Psychology, 68(1), 491-516.
- D’Arcy, J., & Hovav, A. (2009). Does one size fit all? Examining the differential effects of IS security countermeasures. Journal of Business Ethics, 89, 59-71.
- Dincelli, E., & Chengalur-Smith, I. (2020). Choose your own training adventure: designing a gamified SETA artefact for improving information security and privacy through interactive storytelling. European Journal of Information Systems, 29(6), 669-687.
- Jensen, M. L., Dinger, M., Wright, R. T., & Thatcher, J. B. (2017). Training to mitigate phishing attacks using mindfulness techniques. Journal of Management Information Systems, 34(2), 597-626.
- Langer, E. J. (1989). Mindfulness. Addison-Wesley.
- Lee, J. S., Kettinger, W. J., & Zhang, C. (2025). Nothing like the real thing! A randomized field experiment of quasi-mixed reality gamified phishing training. ACM SIGMIS Database: the DATABASE for Advances in Information Systems, 56(1), 61-78.
- Patten, K. P., & Harris, M. (2013). The need to address mobile device security in the higher education IT curriculum. Journal of Information Systems Education, 24(1), 41.
- Tyagi, A. K., Nair, M. M., Niladhuri, S., & Abraham, A. (2020). Security, privacy research issues in various computing platforms: A survey and the road ahead. Journal of Information Assurance & Security, 15(1).
