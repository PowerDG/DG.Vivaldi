





​         [微设计基础架构（MDI）](https://www.cnblogs.com/Leo_wl/p/11557238.html)          



> **阅读目录**
>
> - [一、1.1 解决方案的结构化发展：设计思维](https://www.cnblogs.com/Leo_wl/p/11557238.html#_label0)
> - [二、1.2 分解问题：微块](https://www.cnblogs.com/Leo_wl/p/11557238.html#_label1)
> - [三、1.3 设计思维+微块=微设计基础设施](https://www.cnblogs.com/Leo_wl/p/11557238.html#_label2)
> - [四、2.1.建立：选择团队和环境](https://www.cnblogs.com/Leo_wl/p/11557238.html#_label3)
> - [五、2.2.理解问题](https://www.cnblogs.com/Leo_wl/p/11557238.html#_label4)
> - [六、2.3.定义：分析要求](https://www.cnblogs.com/Leo_wl/p/11557238.html#_label5)
> - [七、2.4.构思：开发可能的解决方案](https://www.cnblogs.com/Leo_wl/p/11557238.html#_label6)
> - [八、2.5.原型设计：创建原型](https://www.cnblogs.com/Leo_wl/p/11557238.html#_label7)
> - [九、2.6.测试：服务的功能检查](https://www.cnblogs.com/Leo_wl/p/11557238.html#_label8)
> - [十、4.1.软件架构](https://www.cnblogs.com/Leo_wl/p/11557238.html#_label9)
> - [undefined、4.2.微服务](https://www.cnblogs.com/Leo_wl/p/11557238.html#_label10)
> - [undefined、4.3.独立系统架构（ISA）](https://www.cnblogs.com/Leo_wl/p/11557238.html#_label11)
> - [undefined、4.4.DevOps](https://www.cnblogs.com/Leo_wl/p/11557238.html#_label12)
> - [undefined、4.5.领域驱动设计（DDD）](https://www.cnblogs.com/Leo_wl/p/11557238.html#_label13)
> - [undefined、4.6.自包含系统（SCS）](https://www.cnblogs.com/Leo_wl/p/11557238.html#_label14)

*微设计基础架构（MDI）*

*了解微设计基础架构（MDI）的概念，它们如何帮助开发，以及它们与DevOps和微服务等技术的关系*。

技术决策既困难又严肃，可以决定项目的成败。如何找到合适的技术栈？“微设计基础架构”（MDI）是一种新方法，它使用“设计思维”中的回忆来开发最佳，易于理解且是公司范围内公认的基础架构或技术堆栈。

技术和基础设施决策具有挑战性，因为必须结合不同的要求（公司，应用，未来的安全等），找到合适的解决方案。在某些情况下，项目的复杂性如此之高，以至于应用了类似项目的最佳实践方法，但这些方法具有不同的背景。这可能导致做出最终不适合应用程序或公司的决策。管理层对成本和速度的期望未得到满足，部署或移交IT运营成为绊脚石。如何防止这种情况？

## 1.原则

解决复杂问题的方法有两种：解决问题的结构化流程，将大问题分解成更小的部分，每个部分都更容易，更清晰地解决。

[返回顶部](https://www.cnblogs.com/Leo_wl/p/11557238.html#_labelTop)

### 1.1 解决方案的结构化发展：设计思维

[设计思维](https://en.wikipedia.org/wiki/Design_thinking)是基于这样的假设：当来自不同学科的人在鼓励创造力的环境中一起工作时，问题会得到更好的解决。他们共同了解人们的需求和动机，以便得出经过多次测试的概念和解决方案。

这个过程的一部分是共鸣，定义，构思，原型设计和测试。例如，设计思维用于开发应用程序或业务单元的数字化。

[返回顶部](https://www.cnblogs.com/Leo_wl/p/11557238.html#_labelTop)

### 1.2 分解问题：微块

IT服务和应用程序的问题在于需求的复杂性，**从开发，集成和部署流程开始，到数据备份，IT安全和数据保护结束。**如果将IT服务划分为单个部分，则特定要求变得更易于管理。

在软件开发中使用类似的方法与微服务一起使用。所谓的垂直将应用程序划分为松散耦合的功能块。这简化了软件开发并提高了解决方案的弹性。但是，不考虑企业，IT操作和数据上下文。

Microblock（参考微服务命名）是与其他块分离的IT服务的一部分，它实现了特殊功能。该块必须满足以下要求：

- 明确功能的定义。
- 不依赖于其他块的技术和基础设施。
- 标准化界面REST，HTML，SQL，DNS等
- 可以独立于其他块进行更改/安装

[返回顶部](https://www.cnblogs.com/Leo_wl/p/11557238.html#_labelTop)

### 1.3 设计思维+微块=微设计基础设施

“微设计基础设施（MDI）将设计思维应用于IT服务的基础设施设计。任何形式的模块化应用程序或IT功能都被视为IT服务。这种IT服务被分解为微块。这些构成了技术决策的基础（在设计思维中称为：Persona）。

具体的基础设施和技术要求源自微块的背景。由于需求较少但必不可少，因此更容易做出合适的技术决策。重要的是要考虑IT服务的每个功能部分，不仅是应用程序模块（微服务）等显而易见的部分，还包括访问或PKI管理，DNS，服务发现，监视，日志记录和数据备份。

MDI流程使用基于设计思维的流程步骤：**理解，定义，构思，原型设计和测试**。在自由展开的意义上，步骤不必按此顺序完全处理，创建一个涵盖尽可能多的解决方案的框架更为重要。

## 2.过程

[返回顶部](https://www.cnblogs.com/Leo_wl/p/11557238.html#_labelTop)

### 2.1.建立：选择团队和环境

MDI团队的组成基于不同的IT学科。目的是能够理解和评估服务和单个微块的所有上下文和要求。这包括IT管理，架构，开发和运营，信息安全和数据保护。该团队应由多种的人才组成，他们在各自的学科和广泛的跨学科方面具有出色的专业知识。

[返回顶部](https://www.cnblogs.com/Leo_wl/p/11557238.html#_labelTop)

### 2.2.理解问题

此阶段的目的是了解流程和数据流，并将IT服务分为微块。此阶段应与软件架构团队并行执行。 MDI团队成员共享，讨论和记录他们看到微块的上下文。

[返回顶部](https://www.cnblogs.com/Leo_wl/p/11557238.html#_labelTop)

### 2.3.定义：分析要求

接口（类型，协议），数据（输入，灵敏度，数量，输出），处理（并行化，编程语言），监视（监视，KPI，日志数据）是定义需求的微块的一部分。此外，还有开发和运营流程（部署和集成）。最后，添加了公司特定的要求（数据保护，信息安全，成本规范）。

[返回顶部](https://www.cnblogs.com/Leo_wl/p/11557238.html#_labelTop)

### 2.4.构思：开发可能的解决方案

对于每个单独的微块，开发了几种技术解决方案的想法，唯一的要求是必须满足规范。通过不断询问，检查技术的选择是否客观和明智。例如，[5-Why](https://en.wikipedia.org/wiki/5_Whys)方法是一种很好的方法。

[返回顶部](https://www.cnblogs.com/Leo_wl/p/11557238.html#_labelTop)

### 2.5.原型设计：创建原型

在开发原型时，应从一开始就使用自动化，因为它简化了微块原型的可重用性和配置。对于每个原型，必须实施运行状况检查和自动安全检查。

[返回顶部](https://www.cnblogs.com/Leo_wl/p/11557238.html#_labelTop)

### 2.6.测试：服务的功能检查

在最后一步中，服务由微块组装而成。功能测试和性能测试显示每个单独的微块是否正确结合。如果此处发生错误，则必须对其进行详细分析以找出原点（例如，忽略的要求，不合适的技术，原型结构中的错误等）。它会跳回到相应的流程步骤并进行更正。如果一切都符合要求，那么没有什么能阻碍上线。

## 3.摘要

“微设计基础架构”的目标是通过一组创意专家为IT服务开发最佳基础架构。由于模块化结构，专注于单个块的分离和高度自动化，所需软件环境的数量可以减少到最小，并且可以降低服务成本。

如果在公司中始终如一地应用此方法，则会创建基于相同技术的微块集群，因为上下文只会稍微改变（通常只是应用程序上下文），因此决策很可能会导致相同的技术。这也是高效的核心技术的基础设施。

## 4.差异

[返回顶部](https://www.cnblogs.com/Leo_wl/p/11557238.html#_labelTop)

### 4.1.软件架构

------

软件架构是指软件系统的基本结构和创建这种结构和系统的学科。
每个结构包括软件元素，它们之间的关系，以及元素和关系的属性。软件系统的体系结构是一种隐喻，类似于建筑物的体系结构。它作为系统和开发项目的蓝图，规划了由设计团队执行的必要任务。

[Wikipedia](https://en.wikipedia.org/wiki/Software_architecture)

------

软件架构设计了软件组件的大致轮廓，不考虑技术方面。 MDI将这方面添加到软件架构中。这意味着Micro Design Infrastructures不是一种替代方案，而是一种应该并行运行的补充流程。

[返回顶部](https://www.cnblogs.com/Leo_wl/p/11557238.html#_labelTop)

### 4.2.微服务

------

微服务是一种软件开发技术，它将应用程序构建为松散耦合服务的集合。在微服务架构中，服务是细粒度的，协议是轻量级的。将应用程序分解为不同的较小服务的好处是它可以提高模块性。这使得应用程序更易于理解，开发，测试，并且对架构入侵更具弹性。

[Wikipedia](https://en.wikipedia.org/wiki/Microservices)

------

微服务主要涉及应用程序开发。在垂直和水平可伸缩性方面，基于应用程序上下文分解应用程序。微设计基础架构（MDI）考虑了一个更全面的上下文，用于定义和分区应用程序，并定义了一个可理解和企业范围内可接受决策的过程。

[返回顶部](https://www.cnblogs.com/Leo_wl/p/11557238.html#_labelTop)

### 4.3.独立系统架构（ISA）

------

ISA是基于经验的最佳实践的集合，特别是微服务和自包含系统以及这些项目所面临的挑战。

[ISA Principles](https://isa-principles.org/)

------

ISA中定义的原则与MDI中的MicroBlocks概念非常相似。此外，Micro Designed Infrastructures定义了一个在上下文中根据这些原则开发IT服务基础架构的过程。

[返回顶部](https://www.cnblogs.com/Leo_wl/p/11557238.html#_labelTop)

### 4.4.DevOps

------

DevOps是两个主要相关趋势碰撞中出现的新术语。第一个也被称为“敏捷基础设施”或“敏捷运营”;它源于将敏捷和精益方法应用于运营工作。第二是对发展和运营人员之间合作价值的更广泛理解[...]。

[The agile admin](https://theagileadmin.com/what-is-devops/)

------

DevOps描述了一种更好合作的文化。这种文化是IT领域任何建设性合作的基础，包括微设计基础设施。然而，MDI提供了有关如何通过此协作解决特定问题的具体流程和原则。

[返回顶部](https://www.cnblogs.com/Leo_wl/p/11557238.html#_labelTop)

### 4.5.领域驱动设计（DDD）

------

DDD是一种通过将实现连接到不断发展的模型来满足复杂需求的软件开发方法。域驱动设计的前提如下：

- 将项目的主要重点放在核心域和域逻辑上;

- 在域的模型上进行复杂的设计;

- 启动技术专家和领域专家之间的创造性协作，以迭代方式完善解决特定领域问题的概念模型。

  [Wikipedia](https://en.wikipedia.org/wiki/Domain-driven_design)

------

DDD在解决问题方面也有类似的方法，特别是通过关注功能和背景。主要地，DDD中的元素是基于域的，与MDI使用的基于功能的微块相反。这意味着Micro Design Infrastructure不是替代方案，而是DDD的补充流程。

[返回顶部](https://www.cnblogs.com/Leo_wl/p/11557238.html#_labelTop)

### 4.6.自包含系统（SCS）

------

SCS方法是一种架构，专注于将功能分离到许多独立系统中，使完整的逻辑系统成为许多小型软件系统的协作。这避免了大型单块体不断增长并最终变得不可维护的问题。在过去几年中，我们已经看到它在许多中型和大型项目中的优势。

[scs-architecture.org](http://scs-architecture.org/)

------

SCS将各个模块视为包含数据逻辑和应用程序逻辑的自治Web应用程序。每个Web应用程序都有自己的用户界面，由不同的团队处理。 Micro  Designed  Infrastructures纯粹从功能角度来看待技术决策。没有理由组合几个功能。此外，MDI定义了一个为IT服务开发无偏见和创造性基础架构的流程。

> 原文：https://dzone.com/articles/take-the-lead-on-technology-decisions-sustainable-1
>
> 作者：[Kai Grotelueschen](https://dzone.com/users/3751958/kgr.html)
>
> 译者：Emma------

​    分类:             [[12\]Architecture](https://www.cnblogs.com/Leo_wl/category/225687.html)